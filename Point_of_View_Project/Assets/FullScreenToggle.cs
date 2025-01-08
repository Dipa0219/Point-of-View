using UnityEngine;
using UnityEngine.SceneManagement;

public class FullScreenToggle : MonoBehaviour
{
    private static FullScreenToggle instance;
    private void Awake()
    {
        // If an instance already exists, destroy this one
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // Set this as the instance and mark it as persistent across scenes
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.F4) || Input.GetKeyDown(KeyCode.F11))
        {
            //print("Toggle Fullscreen");
            if(SceneManager.GetActiveScene().name != "Settings")
                Screen.fullScreen = !Screen.fullScreen;
            else
            {
                print("Sei in settings non puoi cambiare fullscreen");
            }
        }
        
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                //print("Toggle Fullscreen");
                if(SceneManager.GetActiveScene().name != "Settings")
                    Screen.fullScreen = !Screen.fullScreen;
                else
                {
                    print("Sei in settings non puoi cambiare fullscreen");
                }
            }
        }

        
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu_WIP");
        }        
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LevelSelection_WIP");
        }
    }
}