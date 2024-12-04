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
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu_WIP");
        }        
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LevelSelection_WIP");
        }
    }
}