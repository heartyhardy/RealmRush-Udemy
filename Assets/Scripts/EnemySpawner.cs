using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] SpawnPoints spawnPoints;
    [SerializeField] EnemySpawns spawnableEnemies;
    [SerializeField] EnemyDestinations enemyDestinations;

    [SerializeField] [Range(3f, 10f)] float spawnCooldown = 3f;
    [SerializeField] [Range(0,5f)] float cooldownRandomFactor = 2f;
    [SerializeField] [Range(1f, 2f)] float holdTime = 1f;

    [SerializeField] Transform enemyBaseLocation;
    [SerializeField] GameObject enemyBaseSpawningVfx;

    float lastSpawnTime = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SpawnOnCooldown();
	}

    private void SpawnOnCooldown()
    {
        if(Time.time >= lastSpawnTime)
        {
            var selectedSpawnPoint = spawnPoints.GetRandomSpawnPoint();
            var selectedEnemy = spawnableEnemies.GetRandomEnemy();
            var enemyDestination = enemyDestinations.GetRandomDestination();
            float cooldownRandomness = UnityEngine.Random.Range(-cooldownRandomFactor, cooldownRandomFactor + 1);

            PlayBaseSpawningVfx();

            var spawned = Instantiate(
                    selectedEnemy,
                    selectedSpawnPoint.transform.position,
                    selectedEnemy.transform.rotation
                );

            spawned.transform.parent = transform;
            spawned.SetDestination(enemyDestination);
            spawned.Invoke("PushTowardsPlayerBase", holdTime);

            lastSpawnTime = Time.time + spawnCooldown + holdTime + cooldownRandomness ;
        }
    }

    void PlayBaseSpawningVfx()
    {
        GameObject vfx = Instantiate(
                enemyBaseSpawningVfx,
                enemyBaseLocation.position,
                Quaternion.Euler(-90f, 0f, 0f)
            );
        vfx.transform.parent = transform;
        Destroy(vfx, spawnCooldown);
    }
}
