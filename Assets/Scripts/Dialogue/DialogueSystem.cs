using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text _textLabel;
    public Image _headImage;

    [Header("文本文件")]
    public TextAsset _textFile;
    public int _index;
    public float _textSpeed;

    [Header("头像")]
    public List<Sprite> _headList;
    public Text _name;


    public bool _textFinished;

    List<string> _textList = new List<string>();

    void Awake()
    {
        getTextFromFile(_textFile);
        _index = 0;
        _textSpeed = 0.05f;
    }

    private void OnEnable() {
        _textFinished = true;
        StartCoroutine(setTextUI());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(_index == _textList.Count)
            {
                // 当前对话结束
                gameObject.SetActive(false);
                _index = 0;
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
                _textFinished = true;
            }
        }
    }

    void getTextFromFile(TextAsset file)
    {
        _textList.Clear();
        _index = 0;

        var lineData = file.text.Split('\n');

        foreach(var line in lineData)
        {
            _textList.Add(line);
        }
    }

    IEnumerator setTextUI()
    {
        _textFinished = false;
        _textLabel.text = "";
    
        changeHeadImage();
        changeName();

        for(int i = 0; !_textFinished && i < _textList[_index].Length; i++)
        {
            _textLabel.text += _textList[_index][i];

            yield return new WaitForSeconds(_textSpeed);
        }
        _textFinished = true;
        _index++;
    }

    // 改变头像
    void changeHeadImage()
    {
        int headIdx = _textList[_index][0] - 'A';
        if(headIdx >= 0 && headIdx < _headList.Count)
        {
            _headImage.sprite = _headList[headIdx];
        }
        else
        {
            _headImage.sprite = null;
        }
        _index++;
    }

    // 改变名字
    void changeName()
    {
        _name.text = _textList[_index];
        _index++;
    }
}
