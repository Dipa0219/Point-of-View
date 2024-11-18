using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Credits : MonoBehaviour
{
    
    [SerializeField] private Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(LoadLevel("MainMenu_WIP"));
        
        backButton.Select();
    }

    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                backButton.onClick.Invoke();
            }
    }
    
    
        private UnityAction LoadLevel(string s)
        {
            return () => SceneManager.LoadScene(s);
        }
}
