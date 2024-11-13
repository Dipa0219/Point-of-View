using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
   Renderer rend;
   Boolean _isActive;
   public AudioClip soundEffect; // Assegna il suono dal tuo progetto.
   private AudioSource audioSource;

   private void Start()
   {
      rend = GetComponent<Renderer>();
      audioSource = gameObject.AddComponent<AudioSource>();
      audioSource.clip = soundEffect;
   }

   private void OnCollisionEnter(Collision other)
   {
      rend.material.color = Color.red;
      audioSource.Play();
      _isActive = true;
   }
   
   
   private void OnCollisionExit(Collision other)
   {
      rend.material.color = Color.white;
      _isActive = false;
   }

   public void EndGame()
   {
      rend.material.color = Color.green;
   }

   public Boolean isActive()
   {
      return _isActive;
   }
}
