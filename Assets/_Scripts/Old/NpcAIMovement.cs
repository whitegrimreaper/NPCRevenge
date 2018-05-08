using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class NpcAIMovement : MonoBehaviour {
    public Transform targetPosition;
    private Rigidbody2D rb;

    private Seeker seeker;
    //private CharacterController controller;

    public Path path;

    public float speed = 2;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;
    public float repathRate = 1.0f;
    private float lastRepath = float.NegativeInfinity;

    private Vector3 spriteOffset;

	// Use this for initialization
	void Start () {
        // Gets object's Seeker component to get target
        seeker = GetComponent<Seeker>();
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        targetPosition = GetComponent<EnemyScript>().getTarget();

        spriteOffset = new Vector3(0.5f,-0.5f);

        //Start calculating new path, path reqs are asynch
        //Args: (Local position, Target position, fcn to return to
        //seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
		
	}

    public void OnPathComplete(Path p)
    {
        Debug.Log("Path calculated, was there error? " + p.error);
        if (!p.error)
        {
            path = p;
            //reset waypoint counter
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (Time.time > lastRepath + repathRate && seeker.IsDone())
        {
            lastRepath = Time.time;

            // Start a new path to the targetPosition, call the the OnPathComplete function
            // when the path has been calculated (which may take a few frames depending on the complexity)
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }

        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        if (currentWaypoint > path.vectorPath.Count) return;
        if (currentWaypoint == path.vectorPath.Count)
        {
            Debug.Log("End Of Path Reached");
            currentWaypoint++;
            return;
        }

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        Vector3 velocity = dir * speed;
        // Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
        //controller.SimpleMove(velocity);

        rb.AddForce(velocity, ForceMode2D.Impulse);

        // The commented line is equivalent to the one below, but the one that is used
        // is slightly faster since it does not have to calculate a square root
        //if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
        if ((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance * nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
