using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesS : MonoBehaviour, ISingleton
{
    public static EnemiesS inst;
    public bool[] isEnemyAlive;
    private void Awake()
    {
        var numbers = FindObjectsByType<EnemyS>(FindObjectsSortMode.None);
        isEnemyAlive = new bool[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            isEnemyAlive[i] = true;
            numbers[i].number = i;
        }
    }
    public bool AllisDead()
    {
        for (int i = 0; i < isEnemyAlive.Length; i++)
        {
            if (isEnemyAlive[i] == true)
            {
                return false;
            }
        }
        return true;
    }
    public void Init()
    {
        inst = this;
    }
}
