using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationPlatform : MonoBehaviour
{
    
    private bool _activePlatform;
    [SerializeField] private GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        _activePlatform = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Pushable")) return;
        Debug.Log("BOOM");
        if (!_activePlatform)
        {
            _activePlatform = true;
            if (door.activeSelf)
            {
                door.SetActive(false);
                //_active = false;
            }
        }
        else
        {
            return;
        }
        //_audioSource.Play();
        //_children[0].transform.localScale = new Vector3(1, 1.6f, 1);


    }

}
