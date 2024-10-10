using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Audio Event Channel", menuName = "Events/Audio Event Channel")]
public class AudioEventChannelSO : ScriptableObject
{
    public UnityAction<AudioClipEnum> OnEventRaised;

    public void RaiseEvent(AudioClipEnum audio)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(audio);
        else
        {
            Debug.LogWarning("Void event has been raised but there is no UnityAction associated.");
        }
    }

#if UNITY_EDITOR
    [Multiline]
    [Tooltip("Event description (only available when using the Unity Editor).")]
    public string Description = "";
#endif
    
}
