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
    private bool _upVolume, _downVolume;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rogue>(out Rogue rogue))
        {
            _startEvent.Invoke();
            AudioListener.volume = 0;
            AudioListener.pause = false;
            _endVolume = 1;
            _upVolume = true;
            _downVolume = false;
        }
    }

    private void Update()
    {
        if (_upVolume || _downVolume)
        {
            AudioListener.volume = Mathf.MoveTowards(AudioListener.volume, _endVolume, Time.deltaTime * 0.1F);
            if (AudioListener.volume == _endVolume)
            {
                _upVolume = false;
                _downVolume = false;
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
            _endVolume = 0;
            _upVolume = false;
            _downVolume = true;
        }
    }

}
