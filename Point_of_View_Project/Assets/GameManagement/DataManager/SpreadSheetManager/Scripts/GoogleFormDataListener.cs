using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleFormDataListener : MonoBehaviour
{
    [SerializeField] GoogleFormDataEventChannelSO formDataEventChannelSO;

    private GoogleFormSender _googleFormSender;
    private void Awake()
    {
        if (formDataEventChannelSO == null)
        {
            formDataEventChannelSO = Resources.Load<GoogleFormDataEventChannelSO>("GameManagement/DataManager/GoogleFormManager/Events/SubmitCommentEvent");    
        }
        
        _googleFormSender = GetComponent<GoogleFormSender>();
    }

    void OnEnable()
    {
        if (_googleFormSender != null)
        {
            formDataEventChannelSO.OnEventRaised += SubmitDataToForm;
        }
    }

    private void SubmitDataToForm(GoogleFormData formData)
    {
        formDataEventChannelSO.OnEventRaised -= SubmitDataToForm;
        _googleFormSender.SubmitForm(formData);
        formDataEventChannelSO.OnEventRaised += SubmitDataToForm;
    }

    private void OnDisable()
    {
        if (formDataEventChannelSO != null)
        {
            formDataEventChannelSO.OnEventRaised -= SubmitDataToForm;
        }        
    }
    
}
