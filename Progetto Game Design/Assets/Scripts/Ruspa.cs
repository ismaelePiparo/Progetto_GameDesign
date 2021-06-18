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
    [SerializeField] private CutScene _cutScene;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _capo;
        


    private NavMeshAgent _navMeshAgent;

    public bool _isDied = false;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator.enabled = false;
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
            _animator.enabled = false;
            _capo.GetComponent<Animator>().enabled = false;
            _navMeshAgent.enabled = true;
            StartCoroutine("Deactivate");

        }
        else
        {
            if (ThirdPersonUnityCharacterController._playingFlute || ThirdPersonUnityCharacterController._playFlute)
            {
                _navMeshAgent.enabled = false;
                _animator.enabled = false;
                _capo.GetComponent<Animator>().enabled = false;
            }
            else
            {

                StartCoroutine("Wait");

            }
        }
        

        
    }

    

   

    private void FollowTarget()
    {

        _navMeshAgent.enabled = true;
        _capo.GetComponent<Animator>().enabled = true;
        _animator.enabled = true;
        _navMeshAgent.SetDestination(_target.transform.position);
    }


    public IEnumerator Wait()
    {

        yield return new WaitForSecondsRealtime(1);
        FollowTarget();
    }


    public IEnumerator Deactivate()
    {

        _cutScene._nextScene = "TitoliDiCoda";
        yield return new WaitForSecondsRealtime(12);
        
        _cutScene.LaunchCutScene("end");

    }

    
}
