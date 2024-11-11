using System.Collections;
using System.Collections.Generic;
using SaveManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
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
    
    private Button[] buttons_World1;
    private Button[] buttons_World2;
    private Button[] buttons_World3;
    
    private int selectedButtonCol = 0;
    private int selectedButtonRow = 0;
    
    private int buttonLength = 3;

    void Start()
    {
        if (!SaveSystem.CheckIfDataExists())
        {
            SceneManager.LoadScene("InsertName_WIP");
            return;
        }
        
        GameData gameData = ScriptableObject.CreateInstance<GameData>();
        gameData = SaveSystem.LoadGameData();
        
        
        buttons_World1 = new Button[] { b0_1, b0_2, b0_3 };
        buttons_World2 = new Button[] { b1_1, b1_2, b1_3 };
        buttons_World3 = new Button[] { b2_1, b2_2, b2_3 };
        
        
        b0_2.interactable = true;
        b0_3.interactable = true;
        b1_1.interactable = true;
        b1_2.interactable = false;
        b1_3.interactable = false;
        b2_1.interactable = false;
        b2_2.interactable = false;
        b2_3.interactable = false;
        
        
        /*if (gameData.GetLevel(0, 0).GetCompleted() == true)
        {
            b0_2.interactable = true;
        }*/
        b0_1.onClick.AddListener(LoadLevel("0-1"));
        b0_2.onClick.AddListener(LoadLevel("0-2"));
        b0_3.onClick.AddListener(LoadLevel("0-3")); 
        
        
        b1_1.onClick.AddListener(LoadLevel("Level_EndgameTest"));
        b1_2.onClick.AddListener(LoadLevel("Level_EndgameTest"));
        b1_3.onClick.AddListener(LoadLevel("Level_EndgameTest")); 
        
        b2_1.onClick.AddListener(LoadLevel("Level_EndgameTest"));
        b2_2.onClick.AddListener(LoadLevel("Level_EndgameTest"));
        b2_3.onClick.AddListener(LoadLevel("Level_EndgameTest"));  
        
       
        
        /*   PER QUANDO CI SARANNO I LIVELLI VERI
        
        b1_1.onClick.AddListener(LoadLevel("1-1"));
        b1_2.onClick.AddListener(LoadLevel("1-2"));
        b1_3.onClick.AddListener(LoadLevel("1-3")); 
        
        b2_1.onClick.AddListener(LoadLevel("2-1"));
        b2_2.onClick.AddListener(LoadLevel("2-2"));
        b2_3.onClick.AddListener(LoadLevel("2-3"));
        */
        
        if(selectedButtonRow == 0)
            buttons_World1[selectedButtonCol].Select();
        else if(selectedButtonRow == 1)
            buttons_World2[selectedButtonCol].Select();
        else
            buttons_World3[selectedButtonCol].Select();

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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(selectedButtonRow == 0)
            {
                selectedButtonCol = (selectedButtonCol - 1 + buttonLength) % buttonLength;
                if (buttons_World1[selectedButtonCol].interactable == false)
                {
                    selectedButtonCol = 0;
                }
                buttons_World1[selectedButtonCol].Select();
            }
            else if(selectedButtonRow == 1)
            {
                selectedButtonCol = (selectedButtonCol - 1 + buttonLength) % buttonLength;
                if (buttons_World2[selectedButtonCol].interactable == false)
                {
                    selectedButtonCol = 0;
                }
                buttons_World2[selectedButtonCol].Select();
            }
            else
            {
                selectedButtonCol = (selectedButtonCol - 1 + buttonLength) % buttonLength;
                if (buttons_World3[selectedButtonCol].interactable == false)
                {
                    selectedButtonCol = 0;
                }
                buttons_World3[selectedButtonCol].Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(selectedButtonRow == 0)
            {
                selectedButtonCol = (selectedButtonCol + 1) % buttonLength;
                if (buttons_World1[selectedButtonCol].interactable == false)
                {
                    selectedButtonCol = 0;
                }                
                buttons_World1[selectedButtonCol].Select();
            }
            else if(selectedButtonRow == 1)
            {
                selectedButtonCol = (selectedButtonCol + 1) % buttonLength;
                if (buttons_World2[selectedButtonCol].interactable == false)
                {
                    selectedButtonCol = 0;
                }
                buttons_World2[selectedButtonCol].Select();
            }
            else
            {
                selectedButtonCol = (selectedButtonCol + 1) % buttonLength;
                if (buttons_World3[selectedButtonCol].interactable == false)
                {
                    selectedButtonCol = 0;
                }
                buttons_World3[selectedButtonCol].Select();
            }
        }else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(selectedButtonRow == 0)
            {
                
                if (buttons_World2[selectedButtonCol].interactable == false)
                {
                    buttons_World1[selectedButtonCol].Select();
                }
                else
                {
                    selectedButtonRow = 1;
                    buttons_World2[selectedButtonCol].Select(); 
                }

            }else if(selectedButtonRow == 1)
            {
                if (buttons_World3[selectedButtonCol].interactable == false)
                {
                    buttons_World2[selectedButtonCol].Select();
                }
                else
                {
                    selectedButtonRow = 2;
                    buttons_World3[selectedButtonCol].Select(); 
                }

            }else
            {
                selectedButtonRow = 0;
                buttons_World1[selectedButtonCol].Select();
            }
        }else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(selectedButtonRow == 0)
            {
                if (buttons_World3[selectedButtonCol].interactable == false)
                {
                    buttons_World1[selectedButtonCol].Select();
                }
                else
                {
                    selectedButtonRow = 2;
                    buttons_World3[selectedButtonCol].Select();
                }
            }else if(selectedButtonRow == 1)
            {
                if (buttons_World1[selectedButtonCol].interactable == false)
                {
                    buttons_World2[selectedButtonCol].Select();
                }
                else
                {
                    selectedButtonRow = 0;
                    buttons_World1[selectedButtonCol].Select();
                }
            }else
            {
                selectedButtonRow = 1;
                buttons_World2[selectedButtonCol].Select();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if(selectedButtonRow == 0)
            {
                buttons_World1[selectedButtonCol].onClick.Invoke();
            }else if(selectedButtonRow == 1)
            {
                buttons_World2[selectedButtonCol].onClick.Invoke();
            }else
            {
                buttons_World3[selectedButtonCol].onClick.Invoke();
            }
            /*
            b0_1.onClick.Invoke();
            b0_2.onClick.Invoke();
            b0_3.onClick.Invoke();
            b1_1.onClick.Invoke();
            b1_2.onClick.Invoke();
            b1_3.onClick.Invoke();
            b2_1.onClick.Invoke();
            b2_2.onClick.Invoke();
            b2_3.onClick.Invoke();
            */
        }
    }

}
