using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AgentNavigatorS : MonoBehaviour
{
    //=========================================================== Editor
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public RotatingInPointS rotatingInPoint;
    public FightingModeS fightingMode;
    //=========================================================== Editor
    Transform[] waypoints;
    int currentWaypointIndex = 0;
    public bool isAbleTOGo;
    bool isPeaceProcess;
    bool isReached;
    Action[] actionAndMoveToNext;
    private void Awake()
    {
        isAbleTOGo = false;
        waypoints = AllWayPointsS.inst.waypoints;
        actionAndMoveToNext = new Action[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            ChooseAction(i, ref actionAndMoveToNext[i]);
        }
    }
    private void Update()
    {
        //=========================================================== Each point got logic
        if (navMeshAgent.remainingDistance < 0.2f && !navMeshAgent.pathPending && !isReached)
        {
            animator.SetBool("isRunning", false);
            isReached = true;
            rotatingInPoint.isInStabializing = false; 
            navMeshAgent.isStopped = true;
            rotatingInPoint.targetAngle = waypoints[currentWaypointIndex].rotation;
            actionAndMoveToNext[currentWaypointIndex]?.Invoke();
        }
        //=========================================================== Each point got logic
        //=========================================================== Move
        if (isAbleTOGo)
        {
            isAbleTOGo = false;
            isReached = false;
            navMeshAgent.isStopped = false;
            animator.SetBool("isRunning", true);
            rotatingInPoint.ResetRotation();
            rotatingInPoint.isInStabializing = true;
            MoveToNextWaypoint();
        }
        //=========================================================== Move
        //=========================================================== Action processes
        if (isPeaceProcess)
        {
            if (Input.GetMouseButton(0))
            {
                isAbleTOGo = true;
                isPeaceProcess = false;
            }
        }
        //=========================================================== Action processes
    }
    void MoveToNextWaypoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex < waypoints.Length)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
    void ChooseAction //each point has own logic and delegate gets it
        (int i, ref Action action)
    {
        WayPointS.MyType curType = waypoints[i].GetComponent<WayPointS>().curWayPointType;
        if (curType == WayPointS.MyType.start)
        {
            action += StartPoint;
        }
        else if (curType == WayPointS.MyType.peace)
        {
            action += PeacePoint;
        }
        else if (curType == WayPointS.MyType.finish)
        {
            action += FinishPoint;
        }
        else if (curType == WayPointS.MyType.fight)
        {
            action += FightPoint;
        }
    }
    //=========================================================== Point logics
    void StartPoint()
    {
        isAbleTOGo = true;
        Debug.Log("start");
        //empty
    }
    void FinishPoint()
    {
        Debug.Log("finish");
        SceneManager.LoadScene(0);
    }
    void PeacePoint()
    {
        rotatingInPoint.startRotation = navMeshAgent.transform.rotation;
        rotatingInPoint.isRotating = true;
        isPeaceProcess = true;
        Debug.Log("peace");
    }
    void FightPoint()
    {
        animator.SetBool("isShooting", true);
        rotatingInPoint.startRotation = navMeshAgent.transform.rotation;
        rotatingInPoint.isRotating = true;
        fightingMode.isFighting = true;
        Debug.Log("fight");
    }
    //=========================================================== Point logics
}
