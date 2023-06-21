using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Rendering;


[RequireComponent(typeof(SignalReceiver))]
public class SignalTest : MonoBehaviour
{
    public GameObject _grayImage;
    bool _changeState = false;
    HologramBlock hologramBlock;

	// 信号的响应函数
    public void OnReceivePauseSignal()
    {
        gameObject.GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void OnReceiveChangeSignal()
    {
        _changeState = !_changeState;
        if(_grayImage != null)
        {
            _grayImage.SetActive(_changeState);
        }
        
        hologramBlock = VolumeManager.instance.stack.GetComponent<HologramBlock>();
        if (hologramBlock == null){return;}
        hologramBlock.enableEffect = new BoolParameter(_changeState);
    }

    public void OnReceiveStopSignal()
    {
        gameObject.GetComponent<PlayableDirector>().Stop();
        SceneLoader._instance.loadScene("LevelSelect");
    }
}
