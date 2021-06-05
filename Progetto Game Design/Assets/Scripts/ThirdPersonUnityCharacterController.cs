using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonUnityCharacterController : MonoBehaviour
{
    [SerializeField] private Transform _cameraT;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _runSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 3f;

    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _jumpHeight = 3f;

    [SerializeField] private Animator _animator;


    private CharacterController _characterController;
    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;

    private Vector3 _velocity;
    private bool _isGrounded;

    private bool _isRun = false;
    private float _defaultSpeed;
    private bool _playFlute=false;
    private bool _inCollider = false;
    public static bool _playingFlute = false;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _defaultSpeed = _speed;
    }

    
    void Update()
    {

        //Ground Check
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Play_Flute"))
        {
            Debug.Log("Sto suonado");
            _playingFlute = true;
        }
        else
        {

            _playingFlute = false;
            

            if (_isGrounded && _velocity.y < 0f)
            {
                _velocity.y = -2f;
            }



            //GET Input
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            _inputVector = new Vector3(h, 0, v);
            _inputSpeed = Mathf.Clamp(_inputVector.magnitude, 0f, 1f);


            UpdateAnimations();


            //Compute direction According to Camera Orientation
            _targetDirection = _cameraT.TransformDirection(_inputVector).normalized;
            _targetDirection.y = 0f;

            //Rotate Object
            Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDirection, _rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            //Move object along forward
            _characterController.Move(transform.forward * _inputSpeed * _speed * Time.deltaTime);

            //JUMPING
            if (Input.GetKey(KeyCode.Space) && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
            }

            if (Input.GetKeyDown(KeyCode.Z) && _isGrounded && _inCollider)
            {
                _playFlute = true;
                
            }
            else
            {
                _playFlute = false;
            }


            //FALLING
            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);


            if (Input.GetKey(KeyCode.LeftShift))
            {
                
                _isRun = true;
                _speed = _runSpeed;
            }
            else
            {
                _speed = _defaultSpeed;
                _isRun = false;
            }
        }


        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mission"))
        {
            //Debug.Log("sono dentro il collider");
            _inCollider = true;
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Colpita");

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mission"))
        {
            //Debug.Log("sono fuori il collider");
            _inCollider = false;
        }
    }


    private void UpdateAnimations()
    {
        _animator.SetBool("dead", _playFlute);
        if (!_isGrounded)
        {
            _animator.SetFloat("speed", 0f);
        }
        else
        {
            if (_isRun)
            {
                _animator.SetBool("run", true);
            }
            if (!_isRun)
            {
                _animator.SetBool("run", false);
                _animator.SetFloat("speed", _inputSpeed);
            }          
        }
        
        
    }
}
