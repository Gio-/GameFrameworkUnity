using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAudioOnComplete : MonoBehaviour
{
    AudioSource aSource;
    private float checkDelay = .5f; 
    private float timer = 0;
    public void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        if (timer < checkDelay)
            timer += Time.deltaTime;
        else
        {
            if (!aSource.isPlaying) {
                gameObject.SetActive(false);
                timer = 0;
            }
        }
    }
}
