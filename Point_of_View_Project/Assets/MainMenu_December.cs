using System.Collections;
using System.Collections.Generic;
using SaveManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu_December : MonoBehaviour
{
        
    public Button b1;
    public Button b2;
    public Button b3;    
    public Button b4;
    public Button b5;
    
    public Button b6;
    public Button b7;
    public Button b8;
    public Button b9;

    [SerializeField] private GameObject[] stars_1;
    [SerializeField] private GameObject[] stars_2;
    [SerializeField] private GameObject[] stars_3;
    
    [SerializeField] private GameObject[] stars_4;
    [SerializeField] private GameObject[] stars_5;
    
    [SerializeField] private GameObject[] stars_6;
    [SerializeField] private GameObject[] stars_7;
    [SerializeField] private GameObject[] stars_8;
    [SerializeField] private GameObject[] stars_9;

    [SerializeField] private AudioClip soundEffect_scroll; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource_scroll;
    [SerializeField] private AudioClip soundEffect_select; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource_select;
    
        
    public Button[,] levelButtons;
    public const int rows = 3;
    public const int columns = 3;

    private int selectedRow = 0;
    private int selectedCol = 0;
    
    private GameData gameData;
    
    //public float navigationDelay = 0.2f;
    //private float nextNavigationTime = 0f;

    void Start()
    {
        
        if (!SaveSystem.CheckIfDataExists())
        {
            //SceneManager.LoadScene("InsertName_WIP");
            SaveManager.SaveSystem.SaveName("Player1");
            //return;
        }
        
        gameData = ScriptableObject.CreateInstance<GameData>();
        gameData = SaveSystem.LoadGameData();
        
        
                
        levelButtons = new Button[3, 3]
        {
            { b1, b2, b3 },
            { b4, b5, b6 },
            { b7, b8, b9 },
        };

        SetButtonInteractivity();
        AssignLevelListeners();
        InitializeStars();

        SelectButton(selectedRow, selectedCol);
        
        //sound scroll
        _audioSource_scroll = gameObject.AddComponent<AudioSource>();
        _audioSource_scroll.clip = soundEffect_scroll;
        //sound select
        _audioSource_select = gameObject.AddComponent<AudioSource>();
        _audioSource_select.clip = soundEffect_select;
    }

    private void SetButtonInteractivity()
    {
        
        /*if (gameData.GetLevel(0, 0).GetCompleted() == true)
        {
            b0_2.interactable = true;
        }*/
                
        levelButtons[0, 0].interactable = true;
        levelButtons[0, 1].interactable = true;
        levelButtons[0, 2].interactable = true;
        levelButtons[1, 0].interactable = true;
        levelButtons[1, 1].interactable = true;
        levelButtons[1, 2].interactable = true;
        levelButtons[2, 0].interactable = true;
        levelButtons[2, 1].interactable = true;
        levelButtons[2, 2].interactable = true;

        
        /*      
        if(gameData.GetLevel(1, 0).GetCompleted() == true)
        {
            levelButtons[0, 1].interactable = true;
        } else{
            return;    
        }
        if(gameData.GetLevel(1, 1).GetCompleted() == true)
        {
            levelButtons[0, 2].interactable = true;
        } else{
            return;    
        }        
        if(gameData.GetLevel(1, 2).GetCompleted() == true)
        {
            levelButtons[1, 0].interactable = true;
        } else{
            return;    
        }
        
        if(gameData.GetLevel(2, 0).GetCompleted() == true)
        {
            levelButtons[1, 1].interactable = true;
        } else{
            return;    
        }
        if(gameData.GetLevel(2, 1).GetCompleted() == true)
        {
            levelButtons[1, 2].interactable = true;
        } else{
            return;    
        }
        if(gameData.GetLevel(2, 2).GetCompleted() == true)
        {
            levelButtons[2, 0].interactable = true;
        } else{
            return;    
        }
        if(gameData.GetLevel(3, 0).GetCompleted() == true)
        {
            levelButtons[2, 1].interactable = true;
        } else{
            return;    
        }
        if(gameData.GetLevel(3, 1).GetCompleted() == true)
        {
            levelButtons[2, 2].interactable = true;
        } else{
            return;    
        }
        */
    }

    private void AssignLevelListeners()
    {
        levelButtons[0, 0].onClick.AddListener(() => LoadLevel("0-1"));
        levelButtons[0, 1].onClick.AddListener(() => LoadLevel("0-2"));
        levelButtons[0, 2].onClick.AddListener(() => LoadLevel("0-3"));
        
        levelButtons[1, 0].onClick.AddListener(() => LoadLevel("1-1"));
        levelButtons[1, 1].onClick.AddListener(() => LoadLevel("1-2"));
        levelButtons[1, 2].onClick.AddListener(() => LoadLevel("1-3"));
        
        levelButtons[2, 0].onClick.AddListener(() => LoadLevel("2-1"));
        levelButtons[2, 1].onClick.AddListener(() => LoadLevel("2-2"));
        levelButtons[2, 2].onClick.AddListener(() => LoadLevel("2-3"));

        
        /*
            levelButtons[1, 0].onClick.AddListener(() => LoadLevel("1-1"));
            levelButtons[1, 1].onClick.AddListener(() => LoadLevel("1-2"));
            levelButtons[2, 0].onClick.AddListener(() => LoadLevel("2-1"));
            levelButtons[1, 2].onClick.AddListener(() => LoadLevel("1-3"));
            levelButtons[2, 1].onClick.AddListener(() => LoadLevel("2-2"));
            levelButtons[2, 2].onClick.AddListener(() => LoadLevel("2-3"));
        */
    }
    
    private void InitializeStars(){
        StarsActive(stars_1, gameData.GetLevel(1, 0).GetStars());
        StarsActive(stars_2, gameData.GetLevel(1, 1).GetStars());
        StarsActive(stars_3, gameData.GetLevel(1, 2).GetStars());
        
        StarsActive(stars_4, gameData.GetLevel(2, 0).GetStars());
        StarsActive(stars_5, gameData.GetLevel(2, 1).GetStars());
        StarsActive(stars_6, gameData.GetLevel(2, 2).GetStars());
        
        StarsActive(stars_7, gameData.GetLevel(3, 0).GetStars());
        StarsActive(stars_8, gameData.GetLevel(3, 1).GetStars());
        StarsActive(stars_9, gameData.GetLevel(3, 2).GetStars());
    }
    
    
    private void StarsActive(GameObject[] stars, int starsToBe)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < starsToBe);
        }
    }

    
    private void LoadLevel(string sceneName)
    {
        _audioSource_select.Play();
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        HandleNavigation();
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            levelButtons[selectedRow, selectedCol].onClick.Invoke();
        }
    }

    private void HandleNavigation()
    {
        //if (Time.time < nextNavigationTime)
        //            return;
        
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Navigate(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Navigate(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Navigate(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Navigate(0, -1);
        }
    }

    private void Navigate(int colChange, int rowChange)
    {
        _audioSource_scroll.Play();
        int newRow = Mathf.Clamp(selectedRow + rowChange, 0, rows - 1);
        int newCol = (selectedCol + colChange + columns) % columns;

        //if (newRow == 1 && newCol == 2)
         //   newCol = 0;

        if (newRow == 3)
            newRow = 0;
    
        
        if (levelButtons[newRow, newCol].interactable)
        {
            selectedRow = newRow;
            selectedCol = newCol;
            //nextNavigationTime = Time.time + navigationDelay;
        }

        SelectButton(selectedRow, selectedCol);
    }

    private void SelectButton(int row, int col)
    {
        levelButtons[row, col].Select();
    }
    
}