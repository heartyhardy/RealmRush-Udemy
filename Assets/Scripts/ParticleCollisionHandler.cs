using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour {

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "PlayerProjectiles")
            transform.parent.GetComponent<EnemyHP>().TakeDamage(other.GetComponent<DamageDealer>().GetDamage());
    }
}
