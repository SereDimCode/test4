using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllWayPointsS : MonoBehaviour, ISingleton
{
    public static AllWayPointsS inst;
    public Transform[] waypoints;
    public void Init()
    {
        inst = this;
    }
}
