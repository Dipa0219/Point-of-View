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
    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(LoadLevel("Level1"));
        button2.onClick.AddListener(LoadLevel("Level2"));
        button3.onClick.AddListener(LoadLevel("Level_EndgameTest"));
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
        // Activate selected button on Enter or Spacebar
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            button1.onClick.Invoke();
            button2.onClick.Invoke();
            button3.onClick.Invoke();
        }
    }

}
