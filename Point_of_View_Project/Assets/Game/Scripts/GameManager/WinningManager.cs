using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningManager : MonoBehaviour
{
    [SerializeField] private PressurePlateManager pressurePlateManager1;
    [SerializeField] private PressurePlateManager pressurePlateManager2;
    [SerializeField] private Movement player1;
    [SerializeField] private Movement player2;
    [SerializeField] private CameraSwitcher cameraSwitcher;
    [SerializeField] private EndLevelMenu endLevelMenu;
    [SerializeField] private TimerUI timerUI;
    private bool _isFinished;
    
    // Update is called once per frame
    void Update()
    {
        if (!pressurePlateManager1.isActive() || !pressurePlateManager2.isActive()) return;
        pressurePlateManager1.EndGame();
        pressurePlateManager2.EndGame();
        if (_isFinished) {return;}
        print("YOU WON");
        _isFinished = true;
        player1.SetActive(false);
        player2.SetActive(false);
        cameraSwitcher.SetActive(false);
        
        // Saving Timer to later bild the leaderboard
        String time = timerUI.GetTimeAsString();
        timerUI.UnShowTimerUI();
        
        //SceneManager.LoadScene("LevelCompleted");
        endLevelMenu.ShowEndLevelMenu(time);
    }
}
