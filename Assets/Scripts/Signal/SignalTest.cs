using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[RequireComponent(typeof(SignalReceiver))]
public class SignalTest : MonoBehaviour
{
	// 信号的响应函数
    public void OnReceivePauseSignal()
    {
        // gameObject.GetComponent<PlayableDirector>().Pause();
        gameObject.GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void OnReceiveResumeSignal()
    {
        gameObject.GetComponent<PlayableDirector>().Stop();
        SceneLoader._instance.loadScene("LevelSelect");
    }
}
