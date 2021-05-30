using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedThirdPController : MonoBehaviour
{
    [SerializeField] private Transform _cameraT;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _runSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 3f;

    private Animator _animator;

    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;
    private bool _isJumping = false;

    private float lastTime = -1.0f;
    private float _defaultSpeed;

    private bool _isRun = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _defaultSpeed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("_isRun: " + _isRun);
        //Handle the Input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _inputVector = new Vector3(h, 0, v);
        _inputSpeed = Mathf.Clamp(_inputVector.magnitude, 0f, 1f);

        UpdateAnimations();

        //Compute direction According to Camera Orientation
        _targetDirection = _cameraT.TransformDirection(_inputVector).normalized;
        _targetDirection.y = 0f;

        if (_inputSpeed <= 0f || _isJumping)
        {
            _isRun = false;
            return;
        }


        //Calculate rotation vector and rotate
        Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDirection, _rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        //Translate along forward
        transform.Translate(transform.forward * _inputSpeed * _speed * Time.deltaTime, Space.World);

        Debug.DrawRay(transform.position + transform.up * 3f, _targetDirection * 5f, Color.red);
        Debug.DrawRay(transform.position + transform.up * 3f, newDir * 5f, Color.blue);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            if (Time.time - lastTime < 0.2f)
            {
                lastTime = Time.time;
                _speed = _runSpeed;
                _isRun = true;
                Debug.Log("dubletap");
                // turn on running
            }
            else
            {
                lastTime = Time.time;
                _isRun = false;
                _speed = _defaultSpeed;
                Debug.Log("singletap");
                // turn on (or switch to) walking
            }

        }


    }

    private void UpdateAnimations()
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
        _animator.SetBool("dead", Input.GetKey(KeyCode.Z));
    }
}
