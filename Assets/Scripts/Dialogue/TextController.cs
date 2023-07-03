using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TextController : MonoBehaviour
{
    public PlayableDirector _timeline;
    public bool _textFinished;
    protected bool _isAutoPlay;

    [Header("UI组件")]
    public Text _textLabel;
    public Button _skipButton; //跳过按钮
    public Button _autoButton; //自动播放按钮

    [Header("文本文件")]
    public TextAsset _textFile;
    public int _index;
    public float _textSpeed;
    public GameObject _nextLog;
    protected List<string> _textList = new List<string>();

    [Header("音效")]
    public AudioSource _dialogueAudio;

    protected TextController()
    {
        _textSpeed = 0.05f;
    }

    void Awake()
    {
        getTextFromFile(_textFile);
        _index = 0;
    }

    protected virtual void OnEnable() {
        _textFinished = true;
        _isAutoPlay = false;
        if(_skipButton != null)
        {
            _skipButton.gameObject.SetActive(true);
        }
        if(_autoButton != null)
        {
            _autoButton.gameObject.SetActive(true);
        }
        
        playDialogue();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            playDialogue();
        }
    }

    protected virtual void playDialogue()
    {
        if(_index == _textList.Count)
        {
            // 当前对话结束
            endDialogue();
            return;
        }
        if(_textFinished)
        {
            // 文本逐个显示
            StartCoroutine(setTextUI());
        }
        else
        {
            //快进
            _textLabel.text = _textList[_index];
            onEndText();
        }
    }

    // 读取文本文件
    protected virtual void getTextFromFile(TextAsset file)
    {
        _textList.Clear();
        _index = 0;

        var lineData = file.text.Split('\n');

        foreach(var line in lineData)
        {
            _textList.Add(line);
        }
    }

    // 显示文本前
    protected virtual void beforeSetTextUI()
    {

    }

    // 显示文本
    protected virtual IEnumerator setTextUI()
    {
        beforeSetTextUI();
        _textFinished = false;
        _textLabel.text = "";

        for(int i = 0; !_textFinished && i < _textList[_index].Length; i++)
        {
            _textLabel.text += _textList[_index][i];
            // 播放音效
            if(_dialogueAudio != null && (i % 2 == 0))
            {
                _dialogueAudio.PlayOneShot(_dialogueAudio.clip);
            }

            yield return new WaitForSeconds(_textSpeed);
        }
        
        if(!_textFinished) onEndText();
    }

    // 结束当前这段文本
    protected virtual void onEndText()
    {
        _textFinished = true;
        _index++;
        if(_isAutoPlay)
        {
            Invoke("playDialogue", 1.0f);
        }
    }

    // 跳过
    public virtual void endDialogue()
    {
        gameObject.SetActive(false);
        if(_skipButton != null)
        {
            _skipButton.gameObject.SetActive(false);
        }
        if(_autoButton != null)
        {
            _autoButton.gameObject.SetActive(false);
        }
        _index = 0;
        endEvent();
        return;
    }

    // 结束事件
    public virtual void endEvent()
    {

    }

    // 下一段文本
    void activateNextLog()
    {
        _nextLog.SetActive(true);
    }

    // 自动播放
    public void onAutoPlayClick()
    {
        _isAutoPlay = !_isAutoPlay;
    }

}
