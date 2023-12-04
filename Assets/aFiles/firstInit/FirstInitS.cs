using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FirstInitS : MonoBehaviour
{
     public GameObject[] inits;
    private void Awake()
    {
        for (int i = 0; i < inits.Length; i++)
        {
            ISingleton temp = inits[i].GetComponent<ISingleton>();
            temp.Init();
        }
    }
}
public interface ISingleton
{
    public void Init();
}
