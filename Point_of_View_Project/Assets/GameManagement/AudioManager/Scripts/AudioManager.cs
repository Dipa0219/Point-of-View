using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using POLIMIGameCollective;
using UnityEngine.Serialization;

namespace GameManagement.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioMixer audioMixer; 

        [SerializeField] private AudioSource musicSource;

        [SerializeField] private AudioSource sfxSource;

        [FormerlySerializedAs("soundBank")] [SerializeField] private SoundBankSO soundBankSo;

        [FormerlySerializedAs("audioManagerSettings")] [SerializeField] private AudioManagerSettingsSO audioManagerSettingsSo;

        [SerializeField] private bool useMixer = true;
        
        public void SetMasterVolume(float volume)
        {
            if (useMixer)
            {
                // volume for AudioSource is between 0 and 1, 
                // it is from -80 to -20 in the mixer and must be normalized
                float mixerVolume = AudioManager.SliderToDB(volume, audioManagerSettingsSo.MasterMaxDb);
                audioMixer.SetFloat ("MasterVolume", mixerVolume);
            }
            else
            {
                // there is no master volume without mixer
            }
        }

        public void SetMusicVolume(float volume)
        {
            if (useMixer)
            {
                float mixerVolume = AudioManager.SliderToDB(volume, audioManagerSettingsSo.MusicMaxDb);
                audioMixer.SetFloat ("MusicVolume", mixerVolume);
            }
            else
            {
                volume = Mathf.Clamp(volume, 0f, 1f);
                musicSource.volume = volume;
            }
        }

        public void SetSfxVolume(float volume)
        {
            if (useMixer)
            {
                float mixerVolume = AudioManager.SliderToDB(volume, audioManagerSettingsSo.SfxMaxDb);
                audioMixer.SetFloat ("SFXVolume", mixerVolume);
            }
            else
            {
                volume = Mathf.Clamp(volume, 0f, 1f);
                sfxSource.volume = volume;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
                PlayMenuMusicLoop();
            } else if (SceneManager.GetActiveScene().name == "Gameplay")
            {
                PlayGameMusicLoop();
            }
        }

        public void PlayMenuMusicLoop()
        {
            musicSource.clip = soundBankSo.GetMenuMusicLoop();
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlayGameMusicLoop()
        {
            musicSource.clip = soundBankSo.GetGameMusicLoop();
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlayFireSfx()
        {
            sfxSource.PlayOneShot(soundBankSo.GetFireSfx());
        }

        // convert the slider values between 0 and 1 to db values
        // minDB should always be at -80 (silence)
        public static float SliderToDB(float volume, float maxDB=-10, float minDB=-80)
        {
            float dbRange = maxDB - minDB;
            return minDB + volume * dbRange;
        }
        
        //
        public void PlayFromEvent(AudioClipEnum sound)
        {
            switch (sound)
            {
                case AudioClipEnum.Fire:
                    PlayFireSfx();
                    break;
                case AudioClipEnum.MenuBackgroundMusic:
                    PlayMenuMusicLoop();
                    break;
                case AudioClipEnum.GameplayBackgroundMusic:
                    PlayGameMusicLoop();
                    break;
            }
            
        }

    }
}

