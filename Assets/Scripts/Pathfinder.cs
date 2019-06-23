using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] Dictionary<Vector2Int, Waypoint> grid;
    [SerializeField] Waypoint start, end;

    Vector2Int[] directions;
    Queue<Waypoint> breadCrumbs;
    List<Waypoint> exploredPath;

    bool isRunning = false;
    Waypoint cursor;

    // Use this for initialization
    void Awake ()
    {
        InitGrid();
    }

    private void InitGrid()
    {
        Setup();
        CreateGrid();
    }

    public void SetStart(Waypoint start)
    {
        this.start = start;
    }

    public void SetEnd(Waypoint end)
    {
        this.end = end;
    }

    private void BreadthFirstSearch()
    {
        isRunning = true;

        breadCrumbs.Enqueue(start);

        while(breadCrumbs.Count > 0 && isRunning)
        {
            cursor = breadCrumbs.Dequeue();

            //print(cursor);

            HaltIfReachedTheEnd();

            ExpandSearchToAdjacent();

            MarkAsExplored();

        }

        MapPathToAList();
    }

    public List<Waypoint> GetExploredPath(Waypoint start, Waypoint end)
    {
        CleanUp();
        InitGrid();
        SetStart(start);
        SetEnd(end);
        BreadthFirstSearch();

        return exploredPath.ToList();        
    }

    private void CleanUp()
    {
        List<Waypoint> cached = grid.Values.ToList<Waypoint>();

        foreach(Waypoint waypoint in cached)
        {
            waypoint.SetExplored(false);
            waypoint.SetPeerExplorer(null);
        }

        breadCrumbs.Clear();
        grid.Clear();
        exploredPath.Clear();
        
        cursor = null;
        start = null;
        end = null;
    }

    private void MapPathToAList()
    {
        exploredPath.Clear();

        Waypoint cursorPos = end;
        //print(end.GetPeerExplorer().GetPeerExplorer().name + "PEER");
        //Debug.Log("End cursor - " + end.name);

        while (cursorPos)
        {
            exploredPath.Add(cursorPos);
            cursorPos = cursorPos.GetPeerExplorer();
        }

        exploredPath.Reverse();
    }

    private void MarkAsExplored()
    {
        cursor.SetExplored(true);
    }

    private void ExpandSearchToAdjacent()
    {
        if (!isRunning)
            return;
          
        foreach (Vector2Int direction in directions)
        {
            var nextCursor = direction + cursor.GetGridCartesian();

            if (grid.ContainsKey(nextCursor))
            {
                EnqueueNewSearchLocation(nextCursor);
            }
        }
    }

    private void EnqueueNewSearchLocation(Vector2Int nextCursor)
    {
        Waypoint nextGridPos;
        grid.TryGetValue(nextCursor, out nextGridPos);

        if (!nextGridPos.IsExplored() && !breadCrumbs.Contains(nextGridPos))
        {
            breadCrumbs.Enqueue(nextGridPos);
            nextGridPos.SetPeerExplorer(cursor);
           // Debug.Log("Peer cursor:" + cursor);
        }
    }

    private void HaltIfReachedTheEnd()
    {
        if (cursor.Equals(end))
        {
            end = cursor;
            print("Route mapped successfully.");
            isRunning = false;
        }
    }

    private void Setup()
    {
        directions = new Vector2Int[]
        {
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.down,
            Vector2Int.right
        };

        breadCrumbs = new Queue<Waypoint>();
        exploredPath = new List<Waypoint>();
    }

    private void CreateGrid()
    {
        grid = new Dictionary<Vector2Int, Waypoint>();
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int cartesianPos = waypoint.GetGridCartesian();
            bool isOverlapping = grid.ContainsKey(cartesianPos);

            if (isOverlapping)
                Debug.LogWarning("Waypoint exists! " + waypoint);
            else
                grid.Add(cartesianPos, waypoint);
        }
    }

    public Waypoint FetchIfExists(Vector2Int key)
    {
        if (grid.ContainsKey(key))
            return grid[key];
        else return null;
    }
}
