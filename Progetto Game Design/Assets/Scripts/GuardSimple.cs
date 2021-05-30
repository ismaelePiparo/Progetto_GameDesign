﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class GuardSimple : MonoBehaviour
{
    public enum GuardState
    {
        Patrol,
        Chase,
        Attack
    }

    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _workTarget;
    [SerializeField] private float _minChaseDistance = 3f;
    [SerializeField] private float _minAttackDistance = 2f;
    [SerializeField] private float _stoppingDistance = 1f;

    private GuardState _currentGuardState;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentGuardState = GuardState.Patrol;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateState();
        CheckTransition();     
    }

    private void UpdateState()
    {
        switch (_currentGuardState)
        {
            case GuardState.Patrol:
               
                Work();
                break;
            case GuardState.Chase:
                
                FollowTarget();
                break;
            case GuardState.Attack:
                
                Attack();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void CheckTransition()
    {
        GuardState newGuardState = _currentGuardState;

        switch (_currentGuardState)
        {
            case GuardState.Patrol:
                if (IsTargetWithinDistance(_minChaseDistance))
                    newGuardState = GuardState.Chase;
                break;
            
            case GuardState.Chase:
                if (!IsTargetWithinDistance(_minChaseDistance))
                {
                    newGuardState = GuardState.Patrol;
                    break;
                }

                if (IsTargetWithinDistance(_minAttackDistance))
                {
                    newGuardState = GuardState.Attack;
                    break;
                }
                break;
            
            case GuardState.Attack:
                if (!IsTargetWithinDistance(_stoppingDistance))
                    newGuardState = GuardState.Chase;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (newGuardState != _currentGuardState)
        {
            Debug.Log($"Changing State FROM:{_currentGuardState} --> TO:{newGuardState}");
            _currentGuardState = newGuardState;
        }
    }

    private void Attack()
    {
        if (IsTargetWithinDistance(_stoppingDistance))
        {
            Debug.Log("Attacco!");
            _navMeshAgent.isStopped = true;
            _animator.SetBool("walk", false);
            Vector3 targetDirection = _target.transform.position - transform.position;
            targetDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150f* Time.deltaTime);
        }
        else
            FollowTarget();
    }

    private void Work()
    {
        Debug.Log("Work");
        if (IsWorkWithinDistance(_stoppingDistance))
        {
            Debug.Log("Lavoro!");
            _navMeshAgent.isStopped = true;
            _animator.SetBool("walk", false);
            Vector3 targetDirection = _workTarget.transform.position - transform.position;
            targetDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150f * Time.deltaTime);
        }
        else
            ReturnToWork();
    }

    private void FollowTarget()
    {
        _navMeshAgent.isStopped = false;
        _animator.SetBool("walk", true);
        _navMeshAgent.SetDestination(_target.transform.position);
    }

    private void ReturnToWork()
    {
        Debug.Log("ReturnToWork");
        _navMeshAgent.isStopped = false;
        _animator.SetBool("walk", true);
        _navMeshAgent.SetDestination(_workTarget.transform.position);
    }

    private bool IsTargetWithinDistance(float distance)
    {
        return (_target.transform.position - transform.position).sqrMagnitude <= distance * distance;
    }

    private bool IsWorkWithinDistance(float distance)
    {
        return (_workTarget.transform.position - transform.position).sqrMagnitude <= distance * distance;
    }


}
