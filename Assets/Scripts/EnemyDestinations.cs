using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Destinations")]
public class EnemyDestinations : ScriptableObject {

    [SerializeField] List<Waypoint> destinations;

    public List<Waypoint> GetAllDestinations()
    {
        return destinations;
    }

    public Waypoint GetRandomDestination()
    {
        int destinationCount = destinations.Count;
        int randomIndex = UnityEngine.Random.Range(0, destinationCount);

        return (destinations[randomIndex]) ? destinations[randomIndex] : null;
    }
}
