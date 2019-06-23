using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SpawnPoints")]
public class SpawnPoints : ScriptableObject {

    [SerializeField] List<Waypoint> spawnPoints = new List<Waypoint>();

    public List<Waypoint> GetWaypoints()
    {
        return spawnPoints;
    }

    public Waypoint GetRandomSpawnPoint()
    {
        int waypointCount = spawnPoints.Count;
        int randomIndex = UnityEngine.Random.Range(0, waypointCount);

        return (spawnPoints[randomIndex]) ? spawnPoints[randomIndex] : null;
    }
}
