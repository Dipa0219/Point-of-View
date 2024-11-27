using System;
using Game.Scripts.Environment_element;
using UnityEngine;

namespace Game.Scripts.GameManager
{
    public class WinningManager : MonoBehaviour
    {
        [SerializeField] private ExitZone manager1;
        [SerializeField] private ExitZone manager2;
        [SerializeField] private CameraSwitcher cameraSwitcher;
        [SerializeField] private EndLevelMenu endLevelMenu;
        [SerializeField] private TimerUI timerUI;
        private bool _isFinished;
    
        [SerializeField] private FallZone fallZone;
        [SerializeField] private FailedLevel failedLevelUIManager;

    
        // Update is called once per frame
        void Update()
        {
            if(fallZone.IsActive())
            {
                failedLevelUIManager.showFailedLevelMenu();
                cameraSwitcher.DisableSwitcher();
                timerUI.UnShowTimerUI();
            }
        
            if (!manager1.IsActive() || !manager2.IsActive()) return;
            if (_isFinished) {return;}
            print("YOU WON");
            _isFinished = true;
            cameraSwitcher.DisableSwitcher();
        
            // Saving Timer to later build the leaderboard
            String time = timerUI.GetTimeAsString();
            timerUI.UnShowTimerUI();
        
            //SceneManager.LoadScene("LevelCompleted");
            endLevelMenu.ShowEndLevelMenu(time);
        }
    }
}
