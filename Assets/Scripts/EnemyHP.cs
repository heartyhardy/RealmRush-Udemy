using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour {

    [SerializeField] int heathPoints = 200;
    [SerializeField] GameObject deathVfx;
    [SerializeField] GameObject selfDestructVfx;

    public int GetHP()
    {
        return heathPoints;
    }

    public void TakeDamage(int dmg)
    {
        heathPoints -= (heathPoints - dmg < 0) ? heathPoints : dmg;

        if(heathPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayDeathAnimation();
        Destroy(gameObject);
    }

    private void PlayDeathAnimation()
    {
        if (deathVfx)
        {
            GameObject vfx = Instantiate(
                    deathVfx,
                    transform.Find("EnemyMesh").transform.position,
                    Quaternion.identity
                );
            vfx.transform.parent = transform.parent.transform;
            Destroy(vfx, 1f);
        }
    }

    public void SelfDestruct()
    {
        this.heathPoints = 0;

        GameObject vfx = Instantiate(
                selfDestructVfx,
                transform.Find("EnemyMesh").transform.position,
                Quaternion.identity
            );
        vfx.transform.parent = transform.parent.transform;
        Destroy(vfx, 1f);

        Invoke("DamagePlayerBase", .5f);

        Destroy(gameObject, .5f);
    }

    private void DamagePlayerBase()
    {
        PlayerBase playerBase = FindObjectOfType<PlayerBase>();
        if (playerBase)
        {
            DamageDealer damageDealer = GetComponent<DamageDealer>();
            playerBase.TakeDamage(damageDealer.GetDamage());
        }
    }
}
