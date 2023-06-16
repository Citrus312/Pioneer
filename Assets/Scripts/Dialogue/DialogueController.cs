using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : TextController
{
    [Header("UI组件")]

    public Text _name;
    public Image _headImage;

    [Header("头像")]
    public List<Sprite> _headList;
 

    protected override void playDialogue()
    {
        if(_index == _textList.Count)
        {
            // 当前对话结束
            endDialogue();
            return;
        }
        if(_textFinished)
        {
            changeHeadImage();
            changeName();
            // 文本逐个显示
            StartCoroutine(setTextUI());
        }
        else
        {
            //快进
            onEndText();
            _textLabel.text = _textList[_index];
        }
    }

    // 改变头像
    void changeHeadImage()
    {
        int headIdx = base._textList[_index][0] - 'A';
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
