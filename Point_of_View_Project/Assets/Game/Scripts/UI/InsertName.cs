using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class InsertName : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private Button ConfirmButton;

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
        
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu_WIP");
        }
    }
}
