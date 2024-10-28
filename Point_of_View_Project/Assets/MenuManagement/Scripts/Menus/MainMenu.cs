using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace MenuManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        public bool fadeToPlay = true;                          // should it use the fading transition?
        [SerializeField] private float playDelay = 0.5f;        // # seconds before loading the gameplay screen
        [SerializeField] private TransitionFader transitionFaderPrefab;
        
        public void OnSettingsPressed()
        {
            print("SETTINGS");
            SettingsMenu.Open();
        }

        public void OnCreditPressed()
        {
            print("CREDITS");
            CreditsScreen.Open();
        }
        
        public void OnPlayPressed()
        {
            enabled = false;
            this.GameObject().SetActive(false);
            LevelManager.LoadFirstLevel();
        }


        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}
