using System.Collections;
using System.Collections.Generic;
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
        buttons_World1 = new Button[] { b0_1, b0_2, b0_3 };
        buttons_World2 = new Button[] { b1_1, b1_2, b1_3 };
        buttons_World3 = new Button[] { b2_1, b2_2, b2_3 };
        
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
                buttons_World1[selectedButtonCol].Select();
            }
            else if(selectedButtonRow == 1)
            {
                selectedButtonCol = (selectedButtonCol - 1 + buttonLength) % buttonLength;
                buttons_World2[selectedButtonCol].Select();
            }
            else
            {
                selectedButtonCol = (selectedButtonCol - 1 + buttonLength) % buttonLength;
                buttons_World3[selectedButtonCol].Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(selectedButtonRow == 0)
            {
                selectedButtonCol = (selectedButtonCol + 1) % buttonLength;
                buttons_World1[selectedButtonCol].Select();
            }
            else if(selectedButtonRow == 1)
            {
                selectedButtonCol = (selectedButtonCol + 1) % buttonLength;
                buttons_World2[selectedButtonCol].Select();
            }
            else
            {
                selectedButtonCol = (selectedButtonCol + 1) % buttonLength;
                buttons_World3[selectedButtonCol].Select();
            }
        }else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(selectedButtonRow == 0)
            {
                selectedButtonRow = 1;
                buttons_World2[selectedButtonCol].Select();
            }else if(selectedButtonRow == 1)
            {
                selectedButtonRow = 2;
                buttons_World3[selectedButtonCol].Select();
            }else
            {
                selectedButtonRow = 0;
                buttons_World1[selectedButtonCol].Select();
            }
        }else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(selectedButtonRow == 0)
            {
                selectedButtonRow = 2;
                buttons_World3[selectedButtonCol].Select();
            }else if(selectedButtonRow == 1)
            {
                selectedButtonRow = 0;
                buttons_World1[selectedButtonCol].Select();
            }else
            {
                selectedButtonRow = 1;
                buttons_World2[selectedButtonCol].Select();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            b0_1.onClick.Invoke();
            b0_2.onClick.Invoke();
            b0_3.onClick.Invoke();
            b1_1.onClick.Invoke();
            b1_2.onClick.Invoke();
            b1_3.onClick.Invoke();
            b2_1.onClick.Invoke();
            b2_2.onClick.Invoke();
            b2_3.onClick.Invoke();
        }
    }

}
