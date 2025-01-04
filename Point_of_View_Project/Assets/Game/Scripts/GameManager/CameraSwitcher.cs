using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Game.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Movement cube1;
    [SerializeField] private Movement cube2;
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private Camera initialCamera;
    [SerializeField] private TimerUI timerUI;
    
    
    [SerializeField] private Canvas commandsUI;
    //[SerializeField] private CommandsUI commandsUI;
    
    private bool _isActive = true;
    private bool _isActiveCommandsUI = false;
    
    [SerializeField] private AudioClip soundEffect; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource;

    private float initialAlpha;
    
    [SerializeField] private CanvasGroup commandsUICanvasGroup;
      
      
    private void Start()
    {
        // Ensure only one camera is active at start (for example, camera1)
        initialCamera.enabled = true;
        initialCamera.GameObject().SetActive(true);        
        camera1.enabled = false;
        camera1.GameObject().SetActive(false);
        camera2.enabled = false;
        camera2.GameObject().SetActive(false);
        cube1.SetActive(false);
        cube2.SetActive(false);
        
        //commandsUI = GameObject.Find("commandsUI").GetComponent<Canvas>();
        
        commandsUICanvasGroup = commandsUI.GetComponent<CanvasGroup>();
        if (commandsUICanvasGroup == null)
        {
            commandsUICanvasGroup = commandsUI.gameObject.AddComponent<CanvasGroup>();
        }
        initialAlpha = commandsUICanvasGroup.alpha;
        
        //sound
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = soundEffect;
    }
    
    public void SwitchCameras()
    {
        initialCamera.enabled = false;
        initialCamera.GameObject().SetActive(false); 
        camera1.enabled = true;
        camera1.GameObject().SetActive(true);
        camera2.enabled = false;
        camera2.GameObject().SetActive(false);
        cube1.SetActive(false);
        cube2.SetActive(true);
        timerUI.ShowTimerUI();
        
        if(isFirstLevel())
        {
            commandsUI.GameObject().SetActive(true);
            _isActiveCommandsUI = true;
        }
        //commandsUI.GameObject().SetActive(true);
        //_isActiveCommandsUI = true;
    }

    private void Update()
    {
        if ((Input.anyKeyDown && !Input.GetKeyDown(KeyCode.H)) || Input.GetMouseButtonDown(0))
        {
            if (_isActiveCommandsUI)
            {
                StartCoroutine(FadeOutAndDeactivate());
            }
        }        
        
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelManager.ReloadLevel();

            /*if (_isActiveCommandsUI)
            {   
                commandsUI.GameObject().SetActive(false);
                _isActiveCommandsUI = false;
            }*/
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            //print("H key was pressed");
            commandsUI.GameObject().SetActive(true);
            _isActiveCommandsUI = true;
            commandsUICanvasGroup.alpha = initialAlpha;
        }
        // Switch cameras when the "C" key is pressed
        if ( (Input.GetKeyDown(KeyCode.C) && _isActive) || (Input.GetKeyDown(KeyCode.Space) && _isActive) )
        {
            //sound
            PlaySwitchSound();//_audioSource.Play();
            // Toggle the active camera
            if (camera1.enabled)
            {
                camera1.enabled = false;
                camera1.GameObject().SetActive(false);
                camera2.enabled = true;
                camera2.GameObject().SetActive(true);
                cube1.SetActive(true);
                cube2.SetActive(false);
            }
            else
            {
                camera1.enabled = true;
                camera1.GameObject().SetActive(true);
                camera2.enabled = false;
                camera2.GameObject().SetActive(false);
                cube1.SetActive(false);
                cube2.SetActive(true);
            }
            
            /*if (_isActiveCommandsUI)
            {   
                commandsUI.GameObject().SetActive(false);
                _isActiveCommandsUI = false;
            }*/
        }
    }
    
    public void DisableSwitcher()
    {
        _isActive = false;
        cube1.SetActive(false);
        cube2.SetActive(false);
    }
    
    
    private IEnumerator FadeOutAndDeactivate()
    {
        float duration = 1f; 
        float startAlpha = commandsUICanvasGroup.alpha;
        initialAlpha = startAlpha;
        float time = 0;
    
        while (time < duration)
        {
            time += Time.deltaTime;
            commandsUICanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, time / duration);
            yield return null;
        }
    
        commandsUICanvasGroup.alpha = 0;
        commandsUI.gameObject.SetActive(false);
        _isActiveCommandsUI = false;
    }
    
    
    public void PlaySwitchSound()
    {
        _audioSource.Play();
    }    
    
    private bool isFirstLevel()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "0-1";
    }
}
