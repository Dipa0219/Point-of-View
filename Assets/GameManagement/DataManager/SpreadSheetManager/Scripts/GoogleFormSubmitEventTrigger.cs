using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoogleFormSubmitEventTrigger : MonoBehaviour
{
    [SerializeField] GoogleFormDataEventChannelSO formDataEventChannelSO;
    
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private string videogameName = "Space Invaders";
    
    private void Awake()
    {
        if (formDataEventChannelSO == null)
        {
            formDataEventChannelSO = Resources.Load<GoogleFormDataEventChannelSO>("GameManagement/DataManager/GoogleFormManager/Events/SubmitCommentEvent");    
        }
    }

    public void OnSubmitButtonClicked()
    {
        GoogleFormData formData = new GoogleFormData(videogameName, feedbackText.text);
        formDataEventChannelSO.RaiseEvent(formData);
    }
}
