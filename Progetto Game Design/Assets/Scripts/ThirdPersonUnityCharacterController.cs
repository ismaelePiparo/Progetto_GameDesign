using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Image FluteIcon;
    [SerializeField] private int _timeFlute=5;
    [SerializeField] private GameObject _NoFlute;

    private CharacterController _characterController;
    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;

    private Vector3 _velocity;
    private bool _isGrounded;

    private bool _isRun = false;
    private float _defaultSpeed;
    public static bool _playFlute=false;
    public static bool _inCollider = false;
    public static bool _playingFlute = false;

    public static int _IDtarget = 0;


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
          

            SetOpacity(0);
         
            _playingFlute = true;

            _animator.SetBool("dead", false);



        }
        else
        {

            if (FluteIcon.color.a == 0)
            {
                StartCoroutine("RestartFlute", FluteIcon.color.a);
            }

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




            //FLAUTO
            if (Input.GetKeyDown(KeyCode.Mouse0) && _isGrounded && FluteIcon.color.a==1)
            {
                _animator.SetBool("dead", true);
                _playFlute = true;
                //UpdateAnimations();
            }
            else if(Input.GetKeyDown(KeyCode.LeftControl) && _isGrounded && FluteIcon.color.a != 1)
            {
                _playFlute = false;
                StartCoroutine("NoFlute");
            }
            else 
            {
                _playFlute = false;
                //_animator.SetBool("dead", false);
            }


            //FALLING
            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);


            if (Input.GetKey(KeyCode.LeftShift) && _inputSpeed!=0)
            {
                
                _isRun = true;
                _speed = _runSpeed;
            }
            else
            {
                _speed = _defaultSpeed;
                _isRun = false;
            }

            UpdateAnimations();
        }


        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mission"))
        {
            //Debug.Log("sono dentro il collider");
            _IDtarget = other.gameObject.GetComponent<MissionID>().ID;
            _inCollider = true;
            //Debug.Log("operaio da Colpire: "+_IDtarget);
            //Debug.Log("operaio da Colpire: " + other.gameObject.GetComponent<MissionID>().ID);
        }
        //if (other.CompareTag("Enemy"))
        //{
        //    Debug.Log("Colpita");
        //}      

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mission"))
        {
            
            _IDtarget = 0;
            _inCollider = false;
        }
    }


    private void UpdateAnimations()
    {

        
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
        //_animator.SetBool("dead", _playFlute);
        
        
    }

    public void SetOpacity(float a)
    {
        var tempColor = FluteIcon.color;
        tempColor.a = a;
        FluteIcon.color = tempColor;
    }

    IEnumerator RestartFlute(float a)
    {
        
        while (a != 1)
        {
            a = a + 0.2f;
           
            SetOpacity(a);
            yield return new WaitForSecondsRealtime(1);
        }
        yield break;
        
    }

    public IEnumerator NoFlute()
    {
        _NoFlute.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        _NoFlute.SetActive(false);
        yield break;
    }
}
