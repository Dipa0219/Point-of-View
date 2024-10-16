using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public struct GoogleFormData
{
    public string Id;
    public string Data;

    public GoogleFormData(string id, string data)
    {
        this.Id = id;
        this.Data = data;
    }
}

// class to save data to a Google form from Unity
// from https://www.youtube.com/watch?v=WM7f4yN4ZHA
public class GoogleFormSender : MonoBehaviour
{
    [SerializeField] GoogleFormSettingsSO settings;
    
    private bool _completed = false;
    private bool _success = false;
    
    void Awake()
    {
        if (settings == null)
        {
            settings = Resources.Load<GoogleFormSettingsSO>("GameManagement/DataManager/GoogleFormManager/Settings/CommentForm");
        }
    }

    public void SubmitForm(GoogleFormData formData)
    {
        StartCoroutine(Post(formData.Id, formData.Data));
    }
    
    // public void SubmitForm(string id, string data)
    // {
    //     StartCoroutine(Post(id, data));
    // }

    IEnumerator Post(string id, string data)
    {
        WWWForm form = new WWWForm();
        form.AddField(settings.IdEntryField, id);
        form.AddField(settings.DataEntryField, data);

        using (UnityWebRequest www = UnityWebRequest.Post(settings.Url, form))
        {
            print("Start submission");
            _completed = false;
            
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                _success = true;
                Debug.Log("Form Submitted");
            }
            else
            {
                _success = false;
                Debug.Log("Form Submission Error: "+ www.error);
            }
            print("End submission");

            _completed = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SubmitForm(new GoogleFormData("Space Invaders", "Bellissimo"));
        }
    }
    
}
