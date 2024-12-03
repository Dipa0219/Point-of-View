using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BackButton : MonoBehaviour
{
    
    [SerializeField] private Button backButton;
    
    [SerializeField] private AudioClip soundEffect_select; // Assegna il suono dal tuo progetto.
    private AudioSource _audioSource_select; 

    void Start()
    {
        backButton.onClick.AddListener(LoadLevel("MainMenu_WIP"));
        
        backButton.Select();
        
        _audioSource_select = gameObject.AddComponent<AudioSource>();
        _audioSource_select.clip = soundEffect_select;
    }

    void Update()
    {
            //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            //{
            //    backButton.onClick.Invoke();
            //}
    }
    
    
        private UnityAction LoadLevel(string s)
        {
            DontDestroyOnLoad(_audioSource_select.gameObject);
            _audioSource_select.Play(); 
            Destroy(_audioSource_select.gameObject, soundEffect_select.length);
            return () => SceneManager.LoadScene(s);
        }
}
