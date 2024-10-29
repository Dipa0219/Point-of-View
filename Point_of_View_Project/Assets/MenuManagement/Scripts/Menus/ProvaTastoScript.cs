using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    
    public Button button1;
    public Button button2;
    public Button button3;
    
    private Button[] buttons;
    private int selectedButtonIndex = 0;

    void Start()
    {
        buttons = new Button[] { button1, button2, button3 };
        
        button1.onClick.AddListener(LoadLevel("Level1"));
        button2.onClick.AddListener(LoadLevel("Level2"));
        button3.onClick.AddListener(LoadLevel("Level_EndgameTest"));
        
        buttons[selectedButtonIndex].Select();

    }

    private UnityAction LoadLevel(string s)
    {
        return () => SceneManager.LoadScene(s);
    }

    // Update is called once per frame
    void Update()
    {
        HandleNavigation();
    }
    
    
    private void HandleNavigation()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedButtonIndex = (selectedButtonIndex - 1 + buttons.Length) % buttons.Length;
            buttons[selectedButtonIndex].Select();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedButtonIndex = (selectedButtonIndex + 1) % buttons.Length;
            buttons[selectedButtonIndex].Select();
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            button1.onClick.Invoke();
            button2.onClick.Invoke();
            button3.onClick.Invoke();
        }
    }

}
