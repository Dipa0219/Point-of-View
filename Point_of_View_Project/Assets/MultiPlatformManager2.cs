using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

public class MultiPlatformManager2 : MonoBehaviour
{
    [SerializeField] private GameObject Platform;
    
    private void Start() {
        Platform.GetComponent<MovingPlatformUnbound>().StartMoving();
    }
}
