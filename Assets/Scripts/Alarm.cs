using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _duration;
    private Coroutine _coroutine;
    private bool _isAlarm = false;   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out Movement movement))
        {
            if (!_isAlarm && movement.Direction.x > 0)
            {
                _isAlarm = true;
                StartAlarm(_isAlarm);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out Movement movement))
        {
            if (_isAlarm && movement.Direction.x < 0)
            {
                _isAlarm = false;
                StartAlarm(_isAlarm);
            }
        }
    }
    private void StartAlarm(bool isAlarm)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(SetVolumeAlarm(_duration, isAlarm));
        }
        else
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(SetVolumeAlarm(_duration, isAlarm));
        }    
    }
    private IEnumerator SetVolumeAlarm(float duration, bool isAlarm)
    {
        float currentTime = 0;
        float startSound;
        float endSound;
        if (isAlarm)
        {
            startSound = 0;
            endSound = 1;
        }
        else
        {
            startSound = 1;
            endSound = 0;
        }
        _audioSource.volume = startSound;
        _audioSource.Play();
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startSound, endSound, currentTime / duration);
            yield return null;
        }
        _coroutine = null;
    }
}
