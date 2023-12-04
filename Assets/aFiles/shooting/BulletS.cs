using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletS : MonoBehaviour, OptDestroyable
{
    float timer;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (collision.gameObject.GetComponent<EnemyS>() != null)
            {
                EnemyS enemyS = collision.gameObject.GetComponent<EnemyS>();
                enemyS.GetKilled();
            }
            OptS.OptDestroy<BulletS>(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            OptS.OptDestroy<BulletS>(gameObject);
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            OptS.OptDestroy<BulletS>(gameObject);
        }
    }
    public void ResetObject()
    {
        timer = 0f;
    }
}
