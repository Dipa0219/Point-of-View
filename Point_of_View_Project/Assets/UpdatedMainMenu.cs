using System.Collections;
using System.Collections.Generic;
using SaveManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelNavigator : MonoBehaviour
{
    public Button b0_1;
    public Button b0_2;
    public Button b0_3;    
    public Button b1_1;
    public Button b1_2;
    public Button b1_3;    
    public Button b2_1;
    public Button b2_2;
    public Button b2_3;
    
    [SerializeField] private GameObject[] stars_0_1;
    [SerializeField] private GameObject[] stars_0_2;
    [SerializeField] private GameObject[] stars_0_3;
    
    [SerializeField] private GameObject[] stars_1_1;
    [SerializeField] private GameObject[] stars_1_2;
    [SerializeField] private GameObject[] stars_1_3;
    
    [SerializeField] private GameObject[] stars_2_1;
    [SerializeField] private GameObject[] stars_2_2;
    [SerializeField] private GameObject[] stars_2_3;

        
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
            SceneManager.LoadScene("InsertName_WIP");
            return;
        }
        
        gameData = ScriptableObject.CreateInstance<GameData>();
        gameData = SaveSystem.LoadGameData();
        
                
        levelButtons = new Button[rows, columns]
        {
            { b0_1, b0_2, b0_3 },
            { b1_1, b1_2, b1_3 },
            { b2_1, b2_2, b2_3 }
        };

        SetButtonInteractivity();
        AssignLevelListeners();
        InitializeStars();

        SelectButton(selectedRow, selectedCol);
    }

    private void SetButtonInteractivity()
    {
        levelButtons[0, 0].interactable = true;
        levelButtons[0, 1].interactable = false;
        levelButtons[0, 2].interactable = false;
        levelButtons[1, 0].interactable = false;
        levelButtons[1, 1].interactable = false;
        levelButtons[1, 2].interactable = false;
        levelButtons[2, 0].interactable = false;
        levelButtons[2, 1].interactable = false;
        levelButtons[2, 2].interactable = false;
        
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
                
    }

    private void AssignLevelListeners()
    {
        levelButtons[0, 0].onClick.AddListener(() => LoadLevel("0-1"));
        levelButtons[0, 1].onClick.AddListener(() => LoadLevel("0-2"));
        levelButtons[0, 2].onClick.AddListener(() => LoadLevel("0-3"));
        
        levelButtons[1, 0].onClick.AddListener(() => LoadLevel("Level_EndgameTest"));
        levelButtons[1, 1].onClick.AddListener(() => LoadLevel("Level_EndgameTest"));
        levelButtons[1, 2].onClick.AddListener(() => LoadLevel("Level_EndgameTest"));
        levelButtons[2, 0].onClick.AddListener(() => LoadLevel("Level_EndgameTest"));
        levelButtons[2, 1].onClick.AddListener(() => LoadLevel("Level_EndgameTest"));
        levelButtons[2, 2].onClick.AddListener(() => LoadLevel("Level_EndgameTest"));        
        
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
        StarsActive(stars_0_1, gameData.GetLevel(1, 0).GetStars());
        StarsActive(stars_0_2, gameData.GetLevel(1, 1).GetStars());
        StarsActive(stars_0_3, gameData.GetLevel(1, 2).GetStars());
        
        StarsActive(stars_1_1, gameData.GetLevel(2, 0).GetStars());
        StarsActive(stars_1_2, gameData.GetLevel(2, 1).GetStars());
        StarsActive(stars_1_3, gameData.GetLevel(2, 2).GetStars());
        
        StarsActive(stars_2_1, gameData.GetLevel(3, 0).GetStars());
        StarsActive(stars_2_2, gameData.GetLevel(3, 1).GetStars());
        StarsActive(stars_2_3, gameData.GetLevel(3, 2).GetStars());
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
        int newRow = Mathf.Clamp(selectedRow + rowChange, 0, rows - 1);
        int newCol = (selectedCol + colChange + columns) % columns;

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
