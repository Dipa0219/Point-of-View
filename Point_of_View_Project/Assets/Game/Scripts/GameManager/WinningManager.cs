using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningManager : MonoBehaviour
{
    [SerializeField] private PressurePlateManager pressurePlateManager1;
    [SerializeField] private PressurePlateManager pressurePlateManager2;
    [SerializeField] private EndLevelMenu endLevelMenu;
    private bool _isFinished;
    
    // Update is called once per frame
    void Update()
    {
        if (!pressurePlateManager1.isActive() || !pressurePlateManager2.isActive()) return;
        pressurePlateManager1.EndGame();
        pressurePlateManager2.EndGame();
        if (_isFinished) return;
        print("YOU WON");
        _isFinished = true;
        //SceneManager.LoadScene("LevelCompleted");
        endLevelMenu.ShowEndLevelMenu();
    }
}
