using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    const int gridSize = 10;
    bool isExplored = false;
    Waypoint peerExplorer;

    public int GetGridSize()
    {
        return gridSize;
    }

    public bool IsExplored()
    {
        return isExplored;
    }

    public void SetExplored(bool isExplored)
    {
        this.isExplored = isExplored;
    }

    public Waypoint GetPeerExplorer()
    {
        return peerExplorer;
    }

    public void SetPeerExplorer(Waypoint peer)
    {
        peerExplorer = peer;
    }

    public Vector2Int GetGridpos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize) * gridSize,
            Mathf.RoundToInt(transform.position.z / gridSize) * gridSize
            );
    }

    public Vector2Int GetGridCartesian()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }

    public static Vector2Int GetGridCartesian(Transform toGridCordinates)
    {
        return new Vector2Int(
            Mathf.RoundToInt(toGridCordinates.transform.position.x / gridSize),
            Mathf.RoundToInt(toGridCordinates.transform.position.z / gridSize)
            );
    }

    public void SetColor(Color color)
    {
        var diffuseMaterial = transform.Find("Top").GetComponent<MeshRenderer>();
        diffuseMaterial.material.color = color;
    }

    public override bool Equals(object other)
    {
        var waypoint = other as Waypoint;
        return waypoint.GetGridCartesian().x == this.GetGridCartesian().x &&
            waypoint.GetGridCartesian().y == this.GetGridCartesian().y;
    }

    public override int GetHashCode()
    {
        return 624022166 + this.GetGridCartesian().x * this.GetGridCartesian().y;
    }
}
