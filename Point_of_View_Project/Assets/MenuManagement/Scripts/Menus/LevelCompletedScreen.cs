using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuManagement
{
        
    public class LevelCompletedScreen : Menu<LevelCompletedScreen>
    {
        public void OnEnable()
        {
            Time.timeScale = 0f;
        }

        public void OnRestartPressed()
        {
            // delete the current screen
            print("Level manager - pressed restart");
            //base.OnBackPressed();
            LevelManager.ReloadLevel();
        }

        public void OnNextLevelPressed()
        {
            print("Level manager - pressed next");

            //base.OnBackPressed();
            LevelManager.LoadNextLevel();
        }

        public void OnMainMenuPressed()
        {   
            print("Level manager - pressed menu");

            //base.OnBackPressed();
            LevelManager.LoadMainMenuLevel();
        }
        
        public void OnDisable()
        {
            Time.timeScale = 1f;
        }
        
        
        public void Open()
        {
            gameObject.SetActive(true);
        }
    
        public void Close()
        {
            gameObject.SetActive(false);
        }


    }
}
