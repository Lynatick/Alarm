using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private UnityEvent _startEvent;
    [SerializeField] private UnityEvent _stopEvent;
    private float endVolume;
    private bool upVolume, downVolume;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rogue>(out Rogue rogue))
        {
            _startEvent.Invoke();
            AudioListener.volume = 0;
            AudioListener.pause = false;
            endVolume = 1;
            upVolume = true;
            downVolume = false;
        }
    }

    private void Update()
    {
        if (upVolume || downVolume)
        {
            AudioListener.volume = Mathf.MoveTowards(AudioListener.volume, endVolume, Time.deltaTime * 0.1F);
            if (AudioListener.volume == endVolume)
            {
                upVolume = false;
                downVolume = false;
            }
            if (AudioListener.volume == 0) 
                AudioListener.pause = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rogue>(out Rogue rogue))
        {
            _stopEvent.Invoke();
            endVolume = 0;
            upVolume = false;
            downVolume = true;
        }
    }

}
