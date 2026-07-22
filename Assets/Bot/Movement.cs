using System;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    InputAction moving;
    InputAction rotateAction;

    Rigidbody rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxSlopeAngle;

    float rotationDirectionValue;

    bool isMoving, isRotating;

    private Vector2 moveValue;

    private void Start()
    {
        moving = InputManager.Instance.Controls.Player.Move;
        rotateAction = InputManager.Instance.Controls.Player.Rotate;

        rb = GetComponent<Rigidbody>();

        moving.performed += Moving;
        moving.canceled += Moving;

        rotateAction.performed += RotateAction;
        rotateAction.canceled += RotateAction;
    }

    private void OnDisable()
    {
        moving.performed -= Moving;
        moving.canceled -= Moving;

        rotateAction.performed -= RotateAction;
        rotateAction.canceled -= RotateAction;
    }

    private void Update()
    {
    }

    public void FixedUpdate()
    {
        Move();
    }

    void Move()
    { 
        Vector3 moveDirection = (transform.forward * moveValue.y) + (transform.right * moveValue.x).normalized;
        
        if (OnSlope())
        {
            // rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed, ForceMode.Force);
            transform.rotation =  Quaternion.FromToRotation(transform.up, hit.transform.up) * transform.rotation;
        }
        else
        {
            if (transform.rotation != Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f))
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            }
        }
        
        if (!isRotating && moveValue != Vector2.zero)
        {
           


            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
            //rb.linearVelocity = new Vector3(targetVelocity.x, 0, targetVelocity.z);
            //rb.AddForce((transform.forward * moveValue) * moveSpeed);
            if (moveValue == Vector2.zero)
            {
                Debug.Log("STOP");
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (!isMoving)
        {
            if (rotationDirectionValue == 0) return;
            transform.Rotate(Vector3.up * (rotationDirectionValue * rotationSpeed * 10f) * Time.deltaTime);

            rb.linearVelocity = Vector2.zero;
        }
    }

    private RaycastHit hit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.localScale.y / 2f + 0.5f))
           if(hit.normal != Vector3.up)
               return true;
        
        return false;
    }

    private Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, hit.normal).normalized;
    }


    private void Moving(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
        if (context.performed)
            isMoving = true;
        if (context.canceled)
            isMoving = false;
    }


    private void RotateAction(InputAction.CallbackContext context)
    {
        rotationDirectionValue = context.ReadValue<float>();
        if (context.performed)
            isRotating = true;
        if (context.canceled)
            isRotating = false;
    }
}