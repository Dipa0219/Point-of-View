using System;
using Game.Scripts.Environment_element;
using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.GameManager
{
    public class WinningManager : MonoBehaviour
    {
        [SerializeField] private PressurePlateManager pressurePlateManager1;
        [SerializeField] private PressurePlateManager pressurePlateManager2;
        [SerializeField] private CameraSwitcher cameraSwitcher;
        [SerializeField] private EndLevelMenu endLevelMenu;
        [SerializeField] private TimerUI timerUI;
        private bool _isFinished;
    
        [SerializeField] private FallZone fallzone;
        [SerializeField] private FailedLevel failedLevelUIManager;

    
        // Update is called once per frame
        void Update()
        {
            if(fallzone.isActive())
            {
                failedLevelUIManager.showFailedLevelMenu();
                cameraSwitcher.DisableSwitcher();
                timerUI.UnShowTimerUI();
            }
        
            if (!pressurePlateManager1.isActive() || !pressurePlateManager2.isActive()) return;
            pressurePlateManager1.EndGame();
            pressurePlateManager2.EndGame();
            if (_isFinished) {return;}
            print("YOU WON");
            _isFinished = true;
            cameraSwitcher.DisableSwitcher();
        
            // Saving Timer to later bild the leaderboard
            String time = timerUI.GetTimeAsString();
            timerUI.UnShowTimerUI();
        
            //SceneManager.LoadScene("LevelCompleted");
            endLevelMenu.ShowEndLevelMenu(time);
        }
    }
}
