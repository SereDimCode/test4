using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EnemyS : MonoBehaviour
{
    public int number;
    public Animator animator;
    public void GetKilled()
    {
        EnemiesS.inst.isEnemyAlive[number] = false;
        gameObject.SetActive(false);
    }
}
