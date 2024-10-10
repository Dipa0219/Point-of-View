using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameManagement
{
    
    [CreateAssetMenu(menuName = "AudioManager/SoundBank")]
    public class SoundBankSO : ScriptableObject
    {
        public enum SoundEffects
        {
            Explosion,
            Shot
        }
        public AudioClip[] MenuMusicLoop;

        public AudioClip[] GameMusicLoop;
        
        public AudioClip[] FireSfx;
        
        public AudioClip GetMenuMusicLoop()
        {
            if (MenuMusicLoop.Length == 0)
                return null;
            else
                return MenuMusicLoop[UnityEngine.Random.Range(0, MenuMusicLoop.Length)];
        }
        
        public AudioClip GetGameMusicLoop()
        {
            if (GameMusicLoop.Length == 0)
                return null;
            else
                return GameMusicLoop[UnityEngine.Random.Range(0, GameMusicLoop.Length)];
        }
        
        public AudioClip GetFireSfx()
        {
            if (FireSfx.Length == 0)
                return null;
            else 
                return FireSfx[UnityEngine.Random.Range(0, FireSfx.Length)];
        }

        
    }
}

