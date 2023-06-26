using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject audioSourceObj;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceObj = GameObject.Find("ButtonSoundSource");
        audioSource = audioSourceObj.GetComponent<AudioSource>();
        transform.GetComponent<Button>().onClick.AddListener(() => { audioSource.PlayOneShot(audioSource.clip); });
    }
}
