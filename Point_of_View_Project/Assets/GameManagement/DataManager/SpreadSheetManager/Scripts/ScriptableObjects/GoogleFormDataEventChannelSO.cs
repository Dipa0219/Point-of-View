using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Google Form Submit Event", menuName = "Data/Google Form Submit Event")]
public class GoogleFormDataEventChannelSO : ScriptableObject
{
    public UnityAction<GoogleFormData> OnEventRaised;

    public void RaiseEvent(GoogleFormData formData)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(formData);
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
