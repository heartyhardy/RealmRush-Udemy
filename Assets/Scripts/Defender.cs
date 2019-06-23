using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour {

    [SerializeField] Transform turretTop;
    [SerializeField] ParticleSystem gun;

    Transform lookatTarget;
    GameObject currentEnemy;
    Enemy currentEnemyParent;

    bool isEngaged = false;

	// Use this for initialization
	void Awake () {
        Rigidbody towerBase = gameObject.AddComponent<Rigidbody>();
        towerBase.isKinematic = true;

        BoxCollider towerbaseCollider = gameObject.AddComponent<BoxCollider>();
        towerbaseCollider.size = new Vector3(100f, 5f, 100f);
        towerbaseCollider.isTrigger = true;
	}

    // Update is called once per frame
    void Update()
    {
        LookAtEnemy();
        DetectEnemyStatus();
    }

    private void DetectEnemyStatus()
    {
        if (currentEnemy)
        {
            EnemyHP enemyHp = currentEnemy.transform.parent.gameObject.GetComponent<EnemyHP>();
            int hp = enemyHp.GetHP();

            currentEnemyParent = currentEnemy.transform.parent.GetComponent<Enemy>();


            if(hp <= 0)
            {
                Disengage();
            }
        }
        else
        {
            if(isEngaged)
                Disengage();
        }
    }

    private void LookAtEnemy()
    {
        if (lookatTarget && isEngaged)
        {
            turretTop.LookAt(lookatTarget.transform.position);
            turretTop.transform.Find("Gun").transform.LookAt(lookatTarget.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EngageEnemy(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Disengage();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EngageEnemy(other);
        }
    }

    private void EngageEnemy(Collider other)
    {
        GameObject suspect = other.gameObject;
        Enemy suspectParent = suspect.transform.parent.GetComponent<Enemy>();

        if ((!suspectParent.IsSpotted() && !isEngaged) || (suspectParent.IsSpotted() && !isEngaged))
        {
            suspectParent.SetSpotted(true);
            isEngaged = true;
            currentEnemy= other.gameObject;
            currentEnemyParent = suspectParent;
            lookatTarget = other.gameObject.transform;
            OpenFire();
        }


    }

    private void Disengage()
    {
        currentEnemy = null;
        if(currentEnemy)
            currentEnemyParent.SetSpotted(false);
        currentEnemyParent = null;
        isEngaged = false;
        lookatTarget = null;
        Ceasefire();
    }

    private void OpenFire()
    {
        ParticleSystem.EmissionModule emitter = gun.emission;
        emitter.rateOverTime = 1;
    }

    private void Ceasefire()
    {
        ParticleSystem.EmissionModule emitter = gun.emission;
        emitter.rateOverTime = 0;
    }

}
