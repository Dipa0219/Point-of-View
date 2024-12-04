using SaveManager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject endLevelMenuUI; // Assign in the Inspector
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button reloadLevelButton;
    [SerializeField] private Button mainMenuButton;

    private Button[] buttons;
    private int selectedButtonIndex = 0;

    [SerializeField] private GameObject[] stars;
    
    private string finalTime;
    [SerializeField] private TMP_Text text;

    [SerializeField] private AudioClip soundEffect_scroll; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource_scroll;
    [SerializeField] private AudioClip soundEffect_select; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource_select;
    private void Start()
    {
        // Hide the menu initially
        endLevelMenuUI.SetActive(false);
        
        // Get buttons into an array for navigation
        buttons = new Button[] { nextLevelButton, reloadLevelButton, mainMenuButton };
        
        // Subscribe button clicks to functions
        nextLevelButton.onClick.AddListener(NextLevel);
        reloadLevelButton.onClick.AddListener(ReloadLevel);
        mainMenuButton.onClick.AddListener(BackToMainMenu);
        
        //sound scroll
        _audioSource_scroll = gameObject.AddComponent<AudioSource>();
        _audioSource_scroll.clip = soundEffect_scroll;
        //sound select
        _audioSource_select = gameObject.AddComponent<AudioSource>();
        _audioSource_select.clip = soundEffect_select;
    }

    private void Update()
    {
        // If the menu is active, listen for input
        if (endLevelMenuUI.activeSelf)
        {
            HandleNavigation();
        }
    }

    public void ShowEndLevelMenu(string time)
    {
        finalTime = time;

        text.text = text.text + finalTime;
        
        int starsEarned = CalculateStars(finalTime);
        switch (starsEarned)
        {
            case 1:
                stars[0].SetActive(true);
                stars[1].SetActive(false);
                stars[2].SetActive(false);
                break;            
            case 2:
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                stars[2].SetActive(false);
                break;            
            case 3:
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                stars[2].SetActive(true);
                break;
            default:
                stars[0].SetActive(false);
                stars[1].SetActive(false);
                stars[2].SetActive(false);
                break;
        }
        
        endLevelMenuUI.SetActive(true);
        
        if (buttons != null && buttons.Length > 0 && selectedButtonIndex >= 0 && selectedButtonIndex < buttons.Length)
            buttons[selectedButtonIndex].Select(); // Select the first button
        
        print("active scene: "+ SceneManager.GetActiveScene().name);
        // TODO
        // RIMETTERE QUANDO CI SARÀ IL SISTEMA DI SALVATAGGIO 
        //SaveSystem.UpdateLevel(SceneManager.GetActiveScene().name, true, starsEarned, finalTime);
    }

    private int CalculateStars(string s)
    {
        string[] timeParts = s.Split(':');
        int minutes = int.Parse(timeParts[0]);
        int seconds = int.Parse(timeParts[1]);
        
        int secondsCalc = seconds + minutes * 60;
        
        if (secondsCalc > 180)
            return 0;
        if (secondsCalc > 120)
            return 1;
        if (secondsCalc > 60)    
            return 2; 
        return 3;
    }

    private void HandleNavigation()
    {
        // Keyboard navigation
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
            DontDestroyOnLoad(_audioSource_select.gameObject);
            _audioSource_select.Play(); 
            Destroy(_audioSource_select.gameObject, soundEffect_select.length);
            buttons[selectedButtonIndex].onClick.Invoke();
        }
    }

    // Button functions
    private void NextLevel()
    {
        print("Arrivato");
        // Load the next level (assuming level indexing in build settings)
        string sceneName = SceneManager.GetActiveScene().name;
        sceneName = FindNext(sceneName);
        print("Next level: " + sceneName);
        SceneManager.LoadScene(sceneName);

        
        /*
            OLD
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        */
    }
    
    private string FindNext(string sceneName)
    {
        int levelC = int.Parse(sceneName.Substring(2));
        int world = int.Parse(sceneName.Substring(0,1));
        levelC++;
        if (levelC >=4)
        {
            levelC = 1;
            world++;
        }
        
        if (world == 1 && levelC == 2)//(world >=3)
        {
            return ("MainMenu_WIP");
        }
        return world + "-"+levelC;
    }

    
    private void ReloadLevel()
    {
        // Reload current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackToMainMenu()
    {
        // Load Main Menu Scene (replace "MainMenu" with your main menu scene name)
        SceneManager.LoadScene("MainMenu_WIP");
    }
    
}