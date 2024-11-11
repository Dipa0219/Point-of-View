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
        
        print("active scene: ");
        print(SceneManager.GetActiveScene().name);
        
        // TODO
        // RIMETTERE QUANDO CI SARÀ IL SISTEMA DI SALVATAGGIO 
        SaveSystem.UpdateLevel(SceneManager.GetActiveScene().name, true, starsEarned, finalTime);
    }

    private int CalculateStars(string s)
    {
        string[] timeParts = s.Split(':');
        int minutes = int.Parse(timeParts[0]);
        int seconds = int.Parse(timeParts[1]);
        
        if (minutes > 3)
            return 0;
        if (minutes > 2)
            return 1;
        if (minutes > 1)    
            return 2; 
        return 3;
    }

    private void HandleNavigation()
    {
        // Keyboard navigation
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
            buttons[selectedButtonIndex].onClick.Invoke();
        }
    }

    // Button functions
    private void NextLevel()
    {
        // Load the next level (assuming level indexing in build settings)
        string sceneName = SceneManager.GetActiveScene().name;
        sceneName = FindNext2(sceneName);
        print("Next level: " + sceneName);
        SceneManager.LoadScene(sceneName);

        
        /*
            OLD
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        */
    }
    
    private string FindNext(string sceneName)
    {
        //char levelC = char.Parse(sceneName.Substring(5));
        //print(levelC);
        if (sceneName == "Level_EndgameTest" || sceneName == "Level_Build" || sceneName == "LevelCompleted")
        {
            return "Level0";
        }
        
        int level = int.Parse(sceneName.Substring(5));
        //int level = int.Parse(levelC.ToString());
        level++;
        return "Level" + level;
    }
    
    private string FindNext2(string sceneName)
    {
        var parts = sceneName.Split('-');
        int world = int.Parse(parts[0]);
        int level = int.Parse(parts[1]);
        
        if(level == 3)
        {
            world++;
            level = 1;
        }
        
        if(world == 2 && level == 3)
            return "MainMenu_WIP";

        return world + "-" + level;
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