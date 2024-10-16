using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventTrigger : MonoBehaviour
{
    [SerializeField] private AudioEventChannelSO audioEventChannelSO;

    void Awake()
    {
        audioEventChannelSO = Resources.Load<AudioEventChannelSO>("GameManagement/AudioManager/Events/SfxEventChannel");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
#if UNITY_EDITOR            
            print("Raised event "+audioEventChannelSO.name);
#endif            
            audioEventChannelSO.RaiseEvent(AudioClipEnum.Fire);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
#if UNITY_EDITOR            
            print("Raised event "+audioEventChannelSO.name);
#endif            
            audioEventChannelSO.RaiseEvent(AudioClipEnum.MenuBackgroundMusic);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
#if UNITY_EDITOR            
            print("Raised event "+audioEventChannelSO.name);
#endif            
            audioEventChannelSO.RaiseEvent(AudioClipEnum.GameplayBackgroundMusic);
        }
    }
}
