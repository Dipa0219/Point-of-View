using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] waypoints;
    public GameObject target;
    private int i;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance((transform.position), waypoints[i].position) < 0.02f)
        {
            i++;
            if(i >= waypoints.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
}
