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
    // Start is called before the first frame update
    void Start()
    {
        levelsButton.onClick.AddListener(LoadLevel("LevelSelection_WIP"));
        CreditsButton.onClick.AddListener(LoadLevel("Credits"));
        LeaderboardButton.onClick.AddListener(LoadLevel("Leaderboard"));
        SettingsButton.onClick.AddListener(LoadLevel("Settings"));
        QuitButton.onClick.AddListener(Quit);
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
