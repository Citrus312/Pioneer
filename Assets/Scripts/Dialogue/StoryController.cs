using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoryController : TextController
{
    public GameObject _nextLog;

    StoryController()
    {
        _textSpeed = 0.01f;
    }

    protected override void getTextFromFile(TextAsset file)
    {
        _textList.Clear();
        _index = 0;

        var lineData = file.text.ToString();
        _textList.Add(lineData);
    }

    public override void endDialogue()
    {
        base.endDialogue();
        Invoke("activateNextLog", 2.0f);
        return;
    }

    void activateNextLog()
    {
        _nextLog.SetActive(true);
    }
}
