using System;
using SaveManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button LeaderboardButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button QuitButton;
    
    private Button[] buttons;
    private int selectedButtonIndex = 0;
    // Start is called before the first frame update
    
    [SerializeField] private AudioClip soundEffect_scroll; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource_scroll;
    [SerializeField] private AudioClip soundEffect_select; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource_select;
    
    
    void Start()
    {
        /*if (!SaveSystem.CheckIfDataExists())
        {
            SceneManager.LoadScene("InsertName_WIP");
        }*/
        
        
        buttons = new Button[] { levelsButton, LeaderboardButton, SettingsButton, CreditsButton, QuitButton };

        
        levelsButton.onClick.AddListener(LoadLevel("LevelSelection_WIP"));
        CreditsButton.onClick.AddListener(LoadLevel("Credits"));
        LeaderboardButton.onClick.AddListener(LoadLevel("Leaderboard"));
        SettingsButton.onClick.AddListener(LoadLevel("Settings"));
        QuitButton.onClick.AddListener(Quit);
        
        LeaderboardButton.interactable = false;
        
        buttons[selectedButtonIndex].Select();
        
        //sound scroll
        _audioSource_scroll = gameObject.AddComponent<AudioSource>();
        _audioSource_scroll.clip = soundEffect_scroll;
        //sound select
        _audioSource_select = gameObject.AddComponent<AudioSource>();
        _audioSource_select.clip = soundEffect_select;
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
            _audioSource_scroll.Play();
            selectedButtonIndex = (selectedButtonIndex - 1 + buttons.Length) % buttons.Length;
            buttons[selectedButtonIndex].Select();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _audioSource_scroll.Play();
            selectedButtonIndex = (selectedButtonIndex + 1) % buttons.Length;
            buttons[selectedButtonIndex].Select();
        }
        
        // Activate selected button on Enter or Spacebar
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            
            if (buttons[selectedButtonIndex].interactable)
            {
                DontDestroyOnLoad(_audioSource_select.gameObject);
                _audioSource_select.Play(); 
                Destroy(_audioSource_select.gameObject, soundEffect_select.length);
                buttons[selectedButtonIndex].onClick.Invoke();
            }
                
                
            /*
            levelsButton.onClick.Invoke();
            LeaderboardButton.onClick.Invoke();
            SettingsButton.onClick.Invoke();
            QuitButton.onClick.Invoke();
            CreditsButton.onClick.Invoke();
            */
        }
    }

    private void Quit()
    {
        Application.Quit();
    }
}
