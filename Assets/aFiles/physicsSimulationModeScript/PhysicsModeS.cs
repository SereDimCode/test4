using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsModeS : MonoBehaviour
{
    private void Awake()
    {
        if (!Application.isEditor)
        {
            Application.targetFrameRate = 65;
        }
        Time.maximumDeltaTime = 0.033f; //less than 30fps will make game slower
    }
    void Update()
    {
        Physics.Simulate(Time.deltaTime);
    }
}
