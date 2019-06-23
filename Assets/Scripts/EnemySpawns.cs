using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Spawnable Enemies")]
public class EnemySpawns : ScriptableObject {

    [SerializeField] List<Enemy> enemies;

    public List<Enemy> GetAllEnemies()
    {
        return enemies;
    }

    public Enemy GetRandomEnemy()
    {
        int enemyCount = enemies.Count;
        int randomIndex = UnityEngine.Random.Range(0, enemyCount);

        return (enemies[randomIndex]) ? enemies[randomIndex] : null;
    }
}
