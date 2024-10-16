using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Google Form Settings", menuName = "Data/Google Form Settings")]
public class GoogleFormSettingsSO : ScriptableObject
{
    [SerializeField] private string url;
    [SerializeField] private string idEntryField;
    [SerializeField] private string dataEntryField;

    public string Url => url;
    public string IdEntryField => idEntryField;
    public string DataEntryField => dataEntryField;
    
#if UNITY_EDITOR
    [Multiline]
    [Tooltip("Event description (only available when using the Unity Editor).")]
    public string Description = "";
#endif
    
}
