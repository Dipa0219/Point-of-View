using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FailedLevel : MonoBehaviour
{
        //[SerializeField] private FailedLevelMenu failedLevelMenu;
    
    [SerializeField] private GameObject failedLevelMenuUI; 
    private Button[] buttons;
    [SerializeField] private Button reloadLevelButton;
    [SerializeField] private Button mainMenuButton;

    private int selectedButtonIndex = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        failedLevelMenuUI.SetActive(false);
        
        buttons = new Button[] { reloadLevelButton, mainMenuButton };
        reloadLevelButton.onClick.AddListener(ReloadLevel);
        mainMenuButton.onClick.AddListener(BackToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (failedLevelMenuUI.activeSelf)
        {
            HandleNavigation();
        }
    }
    
    
    public void showFailedLevelMenu()
    {
        failedLevelMenuUI.SetActive(true);
        buttons[selectedButtonIndex].Select();
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
        
        
            
    private void ReloadLevel()
    {
        // Reload current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackToMainMenu()
    {
        // Load Main Menu Scene (replace "MainMenu" with your main menu scene name)
        SceneManager.LoadScene("MainMenu");
    }
    
   private void OnCollissionEnter(Collision collision)
    {        
        print("Collided ");
        print("Collided with: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            failedLevelMenuUI.SetActive(true);
            buttons[selectedButtonIndex].Select();
        }
    }
    
    
}
