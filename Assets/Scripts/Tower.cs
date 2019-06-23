using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    TowerBlock towerBlock;

    public void OccupyTowerBlock(TowerBlock towerBlock)
    {
        this.towerBlock = towerBlock;
        towerBlock.OccupyBlock();
    }

    public void ReleaseTowerblock()
    {
        towerBlock.ReleaseBlock();
        this.towerBlock = null;
    }
}
