using Game.Scripts;
using UnityEngine;

public class MultiPlatformManager2 : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject platform2;
    
    private void Start() {
        platform.GetComponent<MovingPlatformUnbound>().StartMoving();
        platform2.GetComponent<MovingPlatformUnbound>().StartMoving();
    }
}
