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
    
    
    [SerializeField] private AudioClip soundEffect_scroll; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource_scroll;
    [SerializeField] private AudioClip soundEffect_select; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource_select;
    

    private int selectedButtonIndex = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        failedLevelMenuUI.SetActive(false);
        
        buttons = new Button[] { reloadLevelButton, mainMenuButton };
        reloadLevelButton.onClick.AddListener(ReloadLevel);
        mainMenuButton.onClick.AddListener(BackToMainMenu);
        
        
        //sound scroll
        _audioSource_scroll = gameObject.AddComponent<AudioSource>();
        _audioSource_scroll.clip = soundEffect_scroll;
        //sound select
        _audioSource_select = gameObject.AddComponent<AudioSource>();
        _audioSource_select.clip = soundEffect_select;
        
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
