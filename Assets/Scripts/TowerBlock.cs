using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlock : MonoBehaviour {

    TowerFactory factory;
    bool isOccupied = false;

    private void Awake()
    {
        factory = FindObjectOfType<TowerFactory>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if(!isOccupied)
            {
                factory.AddTower(this);
            }
            else
            {
                Debug.Log("Max tower limit reaced!");
            }

        }
    }

    public void OccupyBlock()
    {
        isOccupied = true;
    }

    public void ReleaseBlock()
    {
        isOccupied = false;
    }
}
