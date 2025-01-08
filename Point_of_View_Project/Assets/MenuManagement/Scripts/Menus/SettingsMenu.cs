using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManagement.Data;
using GameManagement.Audio;
using SaveManager;

namespace MenuManagement
{
    
    public class SettingsMenu : Menu<SettingsMenu>
    {
        private bool easyModeOn;
        private bool tipsOn;
        private bool isFullScreen;

        [SerializeField] private Toggle EasyModetoggle; 
        [SerializeField] private Toggle Tipstoggle; 
        [SerializeField] private Toggle FullScreentoggle; 
        
        
        private void Start()
        {
            easyModeOn = SaveSystem.checkEasyMode();
            tipsOn = SaveSystem.checkTips();
            
            isFullScreen = Screen.fullScreen;
            
            EasyModetoggle.isOn = easyModeOn;
            Tipstoggle.isOn = tipsOn;
            FullScreentoggle.isOn = isFullScreen;
            
            EasyModetoggle.onValueChanged.AddListener((isChecked) => OnToggleEasyValueChanged(EasyModetoggle, isChecked));
            Tipstoggle.onValueChanged.AddListener((isChecked) => OnToggleTipsValueChanged(Tipstoggle, isChecked));
            FullScreentoggle.onValueChanged.AddListener((isChecked) => OnToggleFullValueChanged(FullScreentoggle, isChecked));
        }

        private void OnToggleFullValueChanged(Toggle fullScreentoggle, bool isChecked)
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        
        private void OnToggleTipsValueChanged(Toggle tipstoggle, bool isChecked)
        {
            SaveSystem.updateTips(isChecked);
        }

        private void OnToggleEasyValueChanged(Toggle easyModetoggle, bool isChecked)
        {
            SaveSystem.updateEasyMode(isChecked);
            
        }
        

    }

}
