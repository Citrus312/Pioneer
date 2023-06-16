using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
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
    protected List<string> _textList = new List<string>();

    protected TextController()
    {
        _textSpeed = 0.05f;
    }

    void Awake()
    {
        getTextFromFile(_textFile);
        _index = 0;
    }

    public void OnEnable() {
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

    protected virtual void beforeSetTextUI()
    {

    }

    protected virtual IEnumerator setTextUI()
    {
        beforeSetTextUI();
        _textFinished = false;
        _textLabel.text = "";

        for(int i = 0; !_textFinished && i < _textList[_index].Length; i++)
        {
            _textLabel.text += _textList[_index][i];

            yield return new WaitForSeconds(_textSpeed);
        }
        
        if(!_textFinished) onEndText();
    }

    protected virtual void onEndText()
    {
        _textFinished = true;
        _index++;
        if(_isAutoPlay)
        {
            Invoke("playDialogue", 1.0f);
        }
    }

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
        return;
    }

    public void onAutoPlayClick()
    {
        _isAutoPlay = !_isAutoPlay;
    }

}
