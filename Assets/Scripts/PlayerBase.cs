using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    [SerializeField] int heathPoints = 3000;
    [SerializeField] Transform playerBaseLocation;
    [SerializeField] GameObject takedmgVfx;
    [SerializeField] GameObject destoryedVfx;


    public int GetBaseHP()
    {
        return heathPoints;
    }

    public void TakeDamage(int dmg)
    {
        heathPoints -= (heathPoints - dmg < 0) ? heathPoints : dmg;

        PlayTakeDamageVfx();

        if(heathPoints <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = .5f;
        FindObjectOfType<LevelLoader>().Invoke("ShowGameOverScreen",1f);
        PlayDestroyedVfx();
        Destroy(gameObject, .5f);
    }

    public void PlayTakeDamageVfx()
    {
        GameObject vfx = Instantiate(
                takedmgVfx,
                playerBaseLocation.position,
                Quaternion.Euler(-90f, 90f, 0f)
            );
        Destroy(vfx, 2f);
    }

    public void PlayDestroyedVfx()
    {
        GameObject vfx = Instantiate(
                destoryedVfx,
                playerBaseLocation.position,
                Quaternion.Euler(-90f, 90f,0f)
            );
        Destroy(vfx, 5f);
    }
}
