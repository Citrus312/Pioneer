using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoryController : TextController
{

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
        return;
    }

    public override void endEvent()
    {
        base.endEvent();
        if(_timeline != null)
        {
            _timeline.Play();
        }
        Invoke("activateNextLog", 4.0f);
    }

    
}
