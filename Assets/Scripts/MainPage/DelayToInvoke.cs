using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//用于非继承MonoBehaviour类调用协程
public class DelayToInvoke
{
    private class TaskBehaviour : MonoBehaviour { }
    private static TaskBehaviour taskBehaviour;

    static DelayToInvoke()
    {
        GameObject gameObject = new GameObject("DelayToInvoke");
        GameObject.DontDestroyOnLoad(gameObject);
        taskBehaviour = gameObject.AddComponent<TaskBehaviour>();
    }

    public static Coroutine StartCoroutine(IEnumerator routine)
    {
        if (routine == null)
        {
            return null;
        }
        return taskBehaviour.StartCoroutine(routine);
    }

    public static void StopCoroutine(ref Coroutine routine)
    {
        if (routine != null)
        {
            taskBehaviour.StopCoroutine(routine);
            routine = null;
        }
    }

    public static Coroutine DelayToInvokeBySecond(Action action, float delaySeconds)
    {
        if (action == null)
        {
            return null;
        }
        return taskBehaviour.StartCoroutine(StartDelayToInvokeBySecond(action, delaySeconds));
    }

    public static Coroutine DelayToInvokeByFrame(Action action, float delayFrames)
    {
        if (action == null)
        {
            return null;
        }
        return taskBehaviour.StartCoroutine(StartDelayToInvokeByFrame(action, delayFrames));
    }

    public static Coroutine ActionLoopByTime(float duration, float interval, Action action)
    {
        if (action == null)
        {
            return null;
        }
        if (duration <= 0 || interval <= 0 || duration < interval)
        {
            return null;
        }
        return taskBehaviour.StartCoroutine(StartActionLoopByTime(duration, interval, action));
    }

    public static Coroutine ActionLoopByCount(int loopCount, float interval, Action action)
    {
        if (action == null)
        {
            return null;
        }
        if (loopCount <= 0 || interval <= 0)
        {
            return null;
        }
        return taskBehaviour.StartCoroutine(StartActionLoopByCount(loopCount, interval, action));
    }

    private static IEnumerator StartDelayToInvokeBySecond(Action action, float delaySeconds)
    {
        if (delaySeconds > 0)
        {
            yield return new WaitForSeconds(delaySeconds);
        }
        else
        {
            yield return null;
        }
        action?.Invoke();
    }

    private static IEnumerator StartDelayToInvokeByFrame(Action action, float delayFrames)
    {
        if (delayFrames > 1)
        {
            for (int i = 0; i < delayFrames; i++)
            {
                yield return null;
            }
        }
        else
        {
            yield return null;
        }
        action?.Invoke();
    }

    private static IEnumerator StartActionLoopByTime(float duration, float interval, Action action)
    {
        yield return new CustomActionLoopByTime(duration, interval, action);
    }

    private static IEnumerator StartActionLoopByCount(int loopCount, float interval, Action action)
    {
        yield return new CustomActionLoopByCount(loopCount, interval, action);
    }

    private class CustomActionLoopByTime : CustomYieldInstruction
    {
        private Action callback;
        private float startTime;
        private float lastTime;
        private float interval;
        private float duration;

        public CustomActionLoopByTime(float _duration, float _interval, Action _callback)
        {
            startTime = Time.time;
            lastTime = Time.time;
            interval = _interval;
            duration = _duration;
            callback = _callback;
        }

        public override bool keepWaiting
        {
            get
            {
                if (Time.time - startTime >= duration)
                {
                    return false;
                }
                else if (Time.time - lastTime >= interval)
                {
                    lastTime = Time.time;
                    callback?.Invoke();
                }
                return true;
            }
        }
    }

    private class CustomActionLoopByCount : CustomYieldInstruction
    {
        private Action callback;
        private float lastTime;
        private float interval;
        private int curCount;
        private int loopCount;

        public CustomActionLoopByCount(int _loopCount, float _interval, Action _callback)
        {
            lastTime = Time.time;
            interval = _interval;
            curCount = 0;
            loopCount = _loopCount;
            callback = _callback;
        }

        public override bool keepWaiting
        {
            get
            {
                if (curCount > loopCount)
                {
                    return false;
                }
                else if (Time.time - lastTime >= interval)
                {
                    lastTime = Time.time;
                    curCount++;
                    callback?.Invoke();
                }
                return true;
            }
        }
    }
}
