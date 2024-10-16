using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement.Audio;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerEventListener : MonoBehaviour
{
    [SerializeField] AudioEventChannelSO audioEventChannel;

    private AudioManager _audioManager;
    private void Awake()
    {
        audioEventChannel = Resources.Load<AudioEventChannelSO>("GameManagement/AudioManager/Events/SfxEventChannel");
        _audioManager = GetComponent<AudioManager>();

    }

    void OnEnable()
    {
        if (audioEventChannel != null)
        {
            audioEventChannel.OnEventRaised += PlaySfx;
        }

    }

    private void PlaySfx(AudioClipEnum sound)
    {
        audioEventChannel.OnEventRaised -= PlaySfx;
        _audioManager.PlayFromEvent(sound);
        audioEventChannel.OnEventRaised += PlaySfx;
    }

    private void OnDisable()
    {
        if (audioEventChannel != null)
        {
            audioEventChannel.OnEventRaised -= PlaySfx;
        }        
    }
}
