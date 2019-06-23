using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    Queue<Tower> towers = new Queue<Tower>();
    [SerializeField] Tower towerPrefab;
    [SerializeField][Range(5,10)] static int maxAllowed = 5;

    private static int towerCount = 0;

    public static int GetMaxAllowedCount()
    {
        return maxAllowed;
    }

    public static int GetTowerCount()
    {
        return towerCount;
    }

    public void AddTower(TowerBlock towerBlock)
    {
        if(GetTowerCount() >= GetMaxAllowedCount())
        {
            print("Dequeing a new Tower!!");
            Tower newTower = towers.Dequeue();
            newTower.ReleaseTowerblock();
            newTower.OccupyTowerBlock(towerBlock);
            newTower.transform.position = GetDefaultBaseV3(towerBlock);
            towers.Enqueue(newTower);
        }
        else
        {
            print("Enqueing a new Tower!!");
            towerCount++;
            Vector3 towerPos = GetDefaultBaseV3(towerBlock);

            Tower newTower = Instantiate(
                    towerPrefab,
                    towerPos,
                    Quaternion.identity
                );

            newTower.transform.parent = towerBlock.transform;
            newTower.OccupyTowerBlock(towerBlock);
            towers.Enqueue(newTower);
        }
    }

    private static Vector3 GetDefaultBaseV3(TowerBlock towerBlock)
    {
        return new Vector3(towerBlock.transform.position.x, 0f, towerBlock.transform.position.z);
    }

    public void RemoveTower()
    {
        towerCount--;
    }
}
