using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] Waypoint destination;
    [SerializeField] float speedOnPath = 2f;
    Waypoint origin;

    List<Waypoint> waypoints;
    Pathfinder pathfinder;

    int currentWaypointIndex = 0;
    bool isRouteExplored = true;
    bool isSpotted = false;

    

    private void Awake()
    {
        Rigidbody enemyRigidbody = transform.Find("EnemyMesh").gameObject.AddComponent<Rigidbody>();
        enemyRigidbody.isKinematic = true;

        transform.Find("EnemyMesh").gameObject.AddComponent<BoxCollider>();

    }
    // Use this for initialization
    void Start () {
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void PushTowardsPlayerBase()
    {
        SetOrigin();
        if (destination)
            EmbarkOnRoute();
    }

    public void SetOrigin()
    {
        Vector2Int cartesianCoordinates = Waypoint.GetGridCartesian(transform);
        Waypoint currentGridPos = pathfinder.FetchIfExists(cartesianCoordinates);

        if(currentGridPos)
        {
            origin=currentGridPos;              
        }
    }

    public void SetDestination(Waypoint destination)
    {
        this.destination = destination;
    }

    public void EmbarkOnRoute()
    {
        waypoints = pathfinder.GetExploredPath(origin, destination);
        currentWaypointIndex = 0;
        isRouteExplored = false;
    }

    // Update is called once per frame
    void Update () {

        if (!isRouteExplored && destination && origin)
            FollowWaypoints();
    }

    private void FollowWaypoints()
    {
        if(currentWaypointIndex <= waypoints.Count -1)
        {
            Vector3 nextWaypoint = waypoints[currentWaypointIndex].transform.position;
            float deltaSpeed = speedOnPath * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, deltaSpeed * Time.deltaTime);

            if(transform.position == nextWaypoint)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            isRouteExplored = true;
            SelfDestruct();
        }
    }

    private void SelfDestruct()
    {
        EnemyHP enemyHP = GetComponent<EnemyHP>();
        enemyHP.SelfDestruct();
    }

    public bool IsSpotted()
    {
        return isSpotted;
    }

    public void SetSpotted(bool isSpotted)
    {
        this.isSpotted = isSpotted;
    }

}
