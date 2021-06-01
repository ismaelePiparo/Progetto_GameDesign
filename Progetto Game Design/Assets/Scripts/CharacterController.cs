using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private bool isMoving, isRunning;
    private Vector2 movementVector;
    [SerializeField] private bool analogMovement = true;
    [SerializeField] private bool capVelocity = false;
    [SerializeField] private bool animationSpeedModulation = false;
    [SerializeField] private float walkSpeedMultiplier = 1.7f;
    [SerializeField] private float runSpeedMultiplier = 2.6f;
    [SerializeField] private float movementDeadZone = 0.15f;
    [SerializeField] private float xBoundLeft = -10f, xBoundRight = 10f, zBoundDown = -10f, zBoundUp = 10f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void GetMovementInput()
    {
        string horizontalAxisName = "Horizontal";
        string verticalAxisName = "Vertical";
        

        float xComponent = Input.GetAxis(horizontalAxisName);
        float zComponent = Input.GetAxis(verticalAxisName);

        

        movementVector = new Vector2(xComponent, zComponent);

        if (capVelocity && movementVector.magnitude > 1.0f)
        {
            movementVector.Normalize();
        }
            
    }

    private float GetRunInput()
    {
       

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            return runSpeedMultiplier;
        }
        else
        {
            isRunning = false;
            return walkSpeedMultiplier;
        }
    }

    private void Move(float multiplier)
    {
        if (movementVector.magnitude >= movementDeadZone)
        {
            isMoving = true;
            RotateTowardsDirection(movementVector);
        }
        else
        {
            isMoving = false;
            return;
        }

        transform.position += new Vector3(movementVector.x, 0.0f, movementVector.y) * multiplier * Time.deltaTime;
    }

    private void RotateTowardsDirection(Vector2 rotateTowards)
    {
        Vector2 unitVector = rotateTowards.normalized;
        float yRotation = Mathf.Atan2(rotateTowards.x, rotateTowards.y) * 57.3f; //57.3 multiplication to convert radians to degrees
        transform.rotation = Quaternion.Euler(0.0f, yRotation, 0.0f);
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isRunning", isRunning);
        if (animationSpeedModulation)
        {
            animator.SetFloat("animationSpeedModulation", movementVector.magnitude);
        }
        else
        {
            animator.SetFloat("animationSpeedModulation", 1.0f);
        }
        //insert here other animator variables
    }

    private void CheckBoundaries()
    {
        //simple check bounds, since movement does not use automatic physics collisions
        if (transform.position.x + movementVector.x < xBoundLeft ||
            transform.position.x + movementVector.x > xBoundRight)
        {
            movementVector = new Vector2(0.0f, movementVector.y);
        }

        if (transform.position.z + movementVector.y < zBoundDown ||
            transform.position.z + movementVector.y > zBoundUp)
        {
            movementVector = new Vector2(movementVector.x, 0.0f);
        }
    }

    private void Update()
    {
        GetMovementInput();
        CheckBoundaries();
        Move(GetRunInput());
        //UpdateAnimator();
    }
}
