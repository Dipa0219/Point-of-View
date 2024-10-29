using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    
    public Button levelsButton;
    public Button CreditsButton;
    public Button LeaderboardButton;
    public Button SettingsButton;
    public Button QuitButton;
    
    private Button[] buttons;
    private int selectedButtonIndex = 0;
    // Start is called before the first frame update
    
    void Start()
    {
        buttons = new Button[] { levelsButton, LeaderboardButton, SettingsButton, CreditsButton, QuitButton };

        
        levelsButton.onClick.AddListener(LoadLevel("LevelSelection_WIP"));
        CreditsButton.onClick.AddListener(LoadLevel("Credits"));
        LeaderboardButton.onClick.AddListener(LoadLevel("Leaderboard"));
        SettingsButton.onClick.AddListener(LoadLevel("Settings"));
        QuitButton.onClick.AddListener(Quit);
        
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
        
        // Activate selected button on Enter or Spacebar
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            levelsButton.onClick.Invoke();
            LeaderboardButton.onClick.Invoke();
            SettingsButton.onClick.Invoke();
            QuitButton.onClick.Invoke();
            CreditsButton.onClick.Invoke();
        }
    }

    private void Quit()
    {
        Application.Quit();
    }
}
