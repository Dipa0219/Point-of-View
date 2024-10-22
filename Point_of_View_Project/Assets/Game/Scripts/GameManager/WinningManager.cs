using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wini : MonoBehaviour
{
    [SerializeField] PressurePlateManager pressurePlateManager1;
    [SerializeField] PressurePlateManager pressurePlateManager2;
    [SerializeField] EndLevelMenu endLevelMenu;
    private Boolean _isFinished;
    
    // Update is called once per frame
    void Update()
    {
        if (pressurePlateManager1.isActive() && pressurePlateManager2.isActive())
        {
            pressurePlateManager1.EndGame();
            pressurePlateManager2.EndGame();
            if (!_isFinished) {
                print("YOU WON");
                _isFinished = true;
                //SceneManager.LoadScene("LevelCompleted");
                endLevelMenu.ShowEndLevelMenu();
            }
        }
    }
}
