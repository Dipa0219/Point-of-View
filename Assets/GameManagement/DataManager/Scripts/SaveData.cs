using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float MasterVolume = 0f;
    public float SfxVolume = 0f;
    public float MusicVolume = 0f;
    public string hashValue;

    public SaveData()
    {
        // default values for audio source volumes
        MasterVolume = .5f;
        SfxVolume = .5f;
        MusicVolume = .75f;
    }
}
