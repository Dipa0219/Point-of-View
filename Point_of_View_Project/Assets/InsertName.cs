using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InsertName : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button ConfirmButton;

    private string _nickname;
    
    void Start()
    {
        ConfirmButton.onClick.AddListener(NameEntered);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmButton.onClick.Invoke();
        }
    }
    
    public void NameEntered()
    {
        _nickname = nameInputField.text;
        print("Name entered: " + _nickname);
        
        
        //SaveManager.SaveSystem.SaveName(_nickname);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu_WIP");
    }
}
