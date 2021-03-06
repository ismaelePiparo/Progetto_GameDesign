using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GuardSimple : MonoBehaviour
{
    public enum GuardState
    {
        Patrol,
        Chase,
        Attack,
        Hit,
        Dead
    }

    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _workTarget;
    [SerializeField] private float _minChaseDistance = 3f;
    [SerializeField] private float _minAttackDistance = 2f;
    [SerializeField] private float _stoppingDistance = 1f;
    [SerializeField] private int _lives = 3;


    [SerializeField] private GameObject _Video;

    private GuardState _currentGuardState;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private bool _inCollider = true;
    public  bool _isDied = false;


    private BoxCollider[] children;

    private int _id = 0;


    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentGuardState = GuardState.Patrol;
        _animator = GetComponent<Animator>();
        children = GetComponentsInChildren<BoxCollider>();
        _id = GetComponentInParent<MissionID>().ID;
        _Video.SetActive(false);
    }

    void Update()
    {

        if (ThirdPersonUnityCharacterController._IDtarget == _id)
        {
            _Video.SetActive(true);
        }
        else
        {
            _Video.SetActive(false);
        }

        //Debug.Log("IdOperaio " + _id);
        UpdateState();
        CheckTransition();
        //Debug.Log("vite: " + _lives);
        if (GameController._decreaseLife && ThirdPersonUnityCharacterController._IDtarget==_id)
        {
            _lives--;
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            
            _isDied = true;
        }
        if (_isDied)
        {
            foreach (BoxCollider box in children)
            {
                box.isTrigger=false;
            }
            StartCoroutine("Deactivate");
        }

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
            case GuardState.Hit:

                Hit();
                break;
            case GuardState.Dead:
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
                //if (IsTargetWithinDistance(_minChaseDistance) && _inCollider)
                //{
                //    newGuardState = GuardState.Chase;
                //    break;
                //}
                if (IsTargetWithinDistance(_minChaseDistance) && _inCollider)
                {
                    if (ThirdPersonUnityCharacterController._inCollider)
                    {
                        newGuardState = GuardState.Chase;
                        //Debug.Log("Pan è nel collider");
                        break;
                    }
                    
                }

                if (ThirdPersonUnityCharacterController._playingFlute)
                {
                    
                    newGuardState = GuardState.Hit;
                    break;
                }
                
                if (_lives == 0)
                {
                    newGuardState = GuardState.Dead;
                    break;
                }

                break;
            
            case GuardState.Chase:
                if (!IsTargetWithinDistance(_minChaseDistance))
                {
                    newGuardState = GuardState.Patrol;
                    break;
                }
                if (!ThirdPersonUnityCharacterController._inCollider)
                {
                    newGuardState = GuardState.Patrol;
                    //Debug.Log("Pan è fuori collider");
                    break;
                }

                if (IsTargetWithinDistance(_minAttackDistance))
                {
                    newGuardState = GuardState.Attack;
                    break;
                }
                if (!_inCollider)
                {
                    newGuardState = GuardState.Patrol;
                    break;
                }
                if (ThirdPersonUnityCharacterController._playingFlute)
                {
                    newGuardState = GuardState.Hit;
                    break;
                }
                if (_lives == 0)
                {
                    newGuardState = GuardState.Dead;
                    break;
                }


                break;

            case GuardState.Hit:
                if (!ThirdPersonUnityCharacterController._playingFlute) 
                {
                    _animator.SetBool("hit", false);
                    newGuardState = GuardState.Patrol;
                    break;
                }
                if (_lives == 0)
                {
                    newGuardState = GuardState.Dead;
                    break;
                }

                break;

            case GuardState.Attack:
                if (!IsTargetWithinDistance(_stoppingDistance))
                {
                    newGuardState = GuardState.Chase;
                    break;
                }
                if (ThirdPersonUnityCharacterController._playingFlute)
                {
                    newGuardState = GuardState.Hit;
                    break;
                }
                if (_lives == 0)
                {
                    newGuardState = GuardState.Dead;
                    break;
                }
                break;

            case GuardState.Dead:
                Debug.Log("Operaio con ID: "+_id+" Sconfitto!");
                if (ThirdPersonUnityCharacterController._playingFlute)
                {
                    _animator.SetBool("hit", true);
                }
                else
                {
                    _animator.SetBool("dead", true);
                }
                
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        if (newGuardState != _currentGuardState)
        {
            //Debug.Log($"Changing State FROM:{_currentGuardState} --> TO:{newGuardState}");
            _currentGuardState = newGuardState;
        }
    }

    private void Attack()
    {
        if (IsTargetWithinDistance(_stoppingDistance))
        {
            //Debug.Log("Attacco!");
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
        
        if (IsWorkWithinDistance(_stoppingDistance))
        {
            //Debug.Log("Lavoro!");
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
        //Debug.Log("ReturnToWork");
        _navMeshAgent.isStopped = false;
        _animator.SetBool("walk", true);
        _navMeshAgent.SetDestination(_workTarget.transform.position);
        if (IsWorkWithinDistance(_stoppingDistance*6))
        {
            _inCollider = true;
        }
    }

    private void Hit()
    {
       
        _navMeshAgent.isStopped = true;

        /*if (_lives == 0 && ThirdPersonUnityCharacterController._playingFlute)
        {
            _animator.SetBool("hit", true);
            _animator.SetBool("dead", true);
            
        }*/
        
        if (ThirdPersonUnityCharacterController._playingFlute)
        {
            _animator.SetBool("hit", true);
        }      
    }

    private bool IsTargetWithinDistance(float distance)
    {
        return (_target.transform.position - transform.position).sqrMagnitude <= distance * distance;
    }

    private bool IsWorkWithinDistance(float distance)
    {
        return (_workTarget.transform.position - transform.position).sqrMagnitude <= distance * distance;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mission"))
        {
            _inCollider = false;
            Debug.Log("_inCollider" + _inCollider);
        }
    }

    public IEnumerator Deactivate()
    {

        yield return new WaitForSecondsRealtime(5);
        this.gameObject.SetActive(false);

    }

    
}
