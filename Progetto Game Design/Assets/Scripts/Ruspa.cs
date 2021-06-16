using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class Ruspa : MonoBehaviour
{

    [SerializeField] private GameObject _target;
    [SerializeField] private int _lives = 3;


    private NavMeshAgent _navMeshAgent;

    public bool _isDied = false;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        if (GameController._decreaseLife)
        {
            _lives--;
        }
        if (_lives == 0)
        {
            _isDied = true;
        }
        

        if (ThirdPersonUnityCharacterController._playingFlute || ThirdPersonUnityCharacterController._playFlute)
        {
            _navMeshAgent.enabled = false;
        }
        else
        {

            FollowTarget();

        }
    }

    

   

    private void FollowTarget()
    {

        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_target.transform.position);
    }

    private bool IsTargetWithinDistance(float distance)
    {
        return (_target.transform.position - transform.position).sqrMagnitude <= distance * distance;
    }



    public IEnumerator Deactivate()
    {

        yield return new WaitForSecondsRealtime(5);
        this.gameObject.SetActive(false);

    }
}
