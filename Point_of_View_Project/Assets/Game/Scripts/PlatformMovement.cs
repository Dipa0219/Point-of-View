using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed;
    public float delay = 1f; // Delay time at each waypoint
    public int startingPoint;
    public Transform[] waypoints;
    public GameObject target;
    
    private int i;
    private bool isWaiting; // To check if the platform is waiting at a waypoint

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[startingPoint].position;
        i = startingPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting)
        {
            // Move the platform if it's not waiting
            if (Vector3.Distance(transform.position, waypoints[i].position) < 0.02f)
            {
                StartCoroutine(WaitAtWaypoint()); // Start waiting at the waypoint
            }
            else
            {
                // Move the platform towards the next waypoint
                transform.position = Vector3.MoveTowards(transform.position, waypoints[i].position, speed * Time.deltaTime);
            }
        }
    }

    // Coroutine to handle waiting at waypoints
    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true; // Stop movement
        yield return new WaitForSeconds(delay); // Wait for the delay duration

        // Move to the next waypoint
        i++;
        if (i >= waypoints.Length)
        {
            i = 0; // Loop back to the first waypoint
        }

        isWaiting = false; // Resume movement
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
