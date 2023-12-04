using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FightingModeS : MonoBehaviour
{
    //=========================================================== Editor
    public GameObject barrel;
    public GameObject bulletPrefab;
    public AgentNavigatorS agentNavigator;
    //=========================================================== Editor
    public bool isFighting;
    float bulletSpeed = 20f;
    private void Awake()
    {
        isFighting = false;
    }
    private void Update()
    {
        if (isFighting && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //=========================================================== Aim
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            Vector3 targetPosition = ray.GetPoint(10f);
            Vector3 targetDirection = Vector3.Normalize(targetPosition - barrel.transform.position);
            //=========================================================== Aim
            //=========================================================== Shot
            GameObject bullet = OptS.OptInstantiate<BulletS>(bulletPrefab, barrel.transform.position);
            bullet.GetComponent<Rigidbody>().velocity = targetDirection * bulletSpeed;
            //=========================================================== Shot
        }
        if (isFighting && EnemiesS.inst.AllisDead())
        {
            isFighting = false;
            agentNavigator.isAbleTOGo = true;
            agentNavigator.animator.SetBool("isShooting", false);
        }
    }
}
