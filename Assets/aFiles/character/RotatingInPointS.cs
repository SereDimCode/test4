using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotatingInPointS : MonoBehaviour
{
    //=========================================================== Editor
    public NavMeshAgent navMeshAgent;
    //=========================================================== Editor
    public Quaternion startRotation;
    public Quaternion targetAngle;
    public bool isRotating;
    public bool isInStabializing;
    float rotatingTimer;
    private void Awake()
    {
        startRotation = Quaternion.identity;
        targetAngle = Quaternion.identity;
        isRotating = false;
        isInStabializing = false;
    }
    private void Update()
    {
        if (isInStabializing)
        {
            FaceTarget();
        }
        if (isRotating)
        {
            rotatingTimer += Time.deltaTime;
            if (rotatingTimer < 2f)
            {
                navMeshAgent.transform.rotation =
                    Quaternion.Lerp(startRotation, targetAngle, rotatingTimer / 2f);
            }
            else
            {
                ResetRotation();
            }
        }
    }
    public void ResetRotation()
    {
        isRotating = false;
        rotatingTimer = 0f;
    }
    void FaceTarget() //make transform.rotation relatable of agent.rotation
    {
        var turnTowardNavSteeringTarget = navMeshAgent.steeringTarget;
        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        if (new Vector3(direction.x, 0, direction.z) != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
