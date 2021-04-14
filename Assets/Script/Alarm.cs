using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private UnityEvent _startEvent;
    [SerializeField] private UnityEvent _stopEvent;
    private float _endVolume;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rogue>(out Rogue rogue))
        {
            _startEvent.Invoke();
            AudioListener.volume = 0;
            AudioListener.pause = false;
            _endVolume = 1;
        }
    }

    private void Update()
    {
        AudioListener.volume = Mathf.MoveTowards(AudioListener.volume, _endVolume, Time.deltaTime * 0.1F);
        if (AudioListener.volume == 0) 
             AudioListener.pause = true;

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rogue>(out Rogue rogue))
        {
            _stopEvent.Invoke();
            _endVolume = 0;
        }
    }

}
