using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class countDownTimer : MonoBehaviour
{
    public float currentTime;
    public float totalTime=60.0f;
    public TextMeshProUGUI timeText;
    void Start()
    {
        currentTime = totalTime;
        timeText =GetComponent<TextMeshProUGUI>();
        InvokeRepeating("updateCountDownTimer", 1f, 1f);
        
    }

    // Update is called once per frame
    void updateCountDownTimer()
    {
        currentTime--;
        timeText.text = "" + currentTime + " S";
        if (currentTime<=0f)
        {
            currentTime = 0;
            CancelInvoke();
        }
        

    }
}
