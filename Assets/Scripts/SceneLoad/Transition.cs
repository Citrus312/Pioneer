using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Transition : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 播放转场前的动画
    /// </summary>
    public void StartTrans(){
        _animator.SetTrigger("Start");
    }

    /// <summary>
    /// 播放转场后的动画
    /// </summary>
    public void EndTrans(){
        _animator.SetTrigger("End");
    }

    /// <summary>
    /// 当前动画是否播放完成
    /// </summary>
    /// <returns></returns>
    public bool IsAnimationDone(){
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            return true;
        else 
            return false;
    }
}
