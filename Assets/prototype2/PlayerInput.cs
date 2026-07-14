using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;


    private float moveValue;
    private Vector2 moveInput;
    //private Vector3 rotationDirection;
    Rigidbody rb;
    //movement
    InputAction moving;
    InputAction leftRotation;
    InputAction rightRotation;
    bool isMoving, isRotating;

    int rotationDirectionValue;

    //pickUp and Drop
    private Rigidbody pickupObject;
    InputAction pickup;
    InputAction dropItem;
    InputAction Throw;
    [SerializeField] private float throwForce;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private FixedJoint pickupJoint;

    //HANDS
    [SerializeField] private GameObject hands;
    [Range(-60, 0)]
    private float handAngle;
    private float handRotationValue;
    InputAction handsRaise;
    InputAction handsLower;

    private void Start()
    {
        Debug.Log("BOT");
        moving = InputManager.Instance.Controls.Player.Move;

        leftRotation = InputManager.Instance.Controls.Player.LeftRotate;
        rightRotation = InputManager.Instance.Controls.Player.RightRotate;

        pickup = InputManager.Instance.Controls.Player.PickUp;
        dropItem = InputManager.Instance.Controls.Player.DropItem;
        Throw = InputManager.Instance.Controls.Player.Throw;

        handsRaise = InputManager.Instance.Controls.Player.Hands;
        handsLower = InputManager.Instance.Controls.Player.Hands;

        rb = GetComponent<Rigidbody>();

        moving.performed += Moving;
        moving.canceled += StopMoving;

        leftRotation.performed += RotateLeft;
        leftRotation.canceled += StopRotation;

        rightRotation.performed += RotateRight;
        rightRotation.canceled += StopRotation;

        pickup.performed += PickUpItem;
        dropItem.performed += DropItem;
        Throw.performed += ThrowItem;

        handsRaise.performed += RaiseHands;
        handsRaise.canceled += RaiseHands;
    }



    private void OnDisable()
    {
        moving.performed -= Moving;
        moving.canceled -= StopMoving;

        leftRotation.performed -= RotateLeft;
        leftRotation.canceled -= StopRotation;
        rightRotation.performed -= RotateRight;
        rightRotation.canceled -= StopRotation;

        pickup.performed -= PickUpItem;
        dropItem.performed -= DropItem;

    }
    private void Update()
    {
        Movement();
        HandAngle();


    }

    void Movement()
    {
        if (!isRotating)
        {
            rb.linearVelocity = transform.forward * moveValue * moveSpeed;
            //rb.AddForce((transform.forward * moveValue) * moveSpeed);
            if (moveValue == 0)
            {
                Debug.Log("STOP");
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (!isMoving)
        {
            transform.Rotate(Vector3.up * (rotationDirectionValue * rotationSpeed * 10f) * Time.deltaTime);
            rb.linearVelocity = Vector2.zero;
        }
    }

    void HandAngle()
    {
        hands.transform.Rotate(Vector3.right * (handRotationValue * rotationSpeed * 10f) * Time.deltaTime);
    }
    private void Moving(InputAction.CallbackContext context)
    {

        moveValue = context.ReadValue<float>();

        isMoving = true;

    }
    private void StopMoving(InputAction.CallbackContext context)
    {
        moveValue = 0;
        isMoving = false;
    }
    private void RotateLeft(InputAction.CallbackContext context)
    {
        rotationDirectionValue = -1;
        isRotating = true;
    }
    private void RotateRight(InputAction.CallbackContext context)
    {
        rotationDirectionValue = 1;
        isRotating = true;
    }

    private void StopRotation(InputAction.CallbackContext context)
    {
        isRotating = false;
    }

    private void PickUpItem(InputAction.CallbackContext context)
    {
        if (pickupObject != null) return;
        Debug.Log("Try pickup");

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayDistance, PickupLayer))
        {
            Debug.Log("Hit");
            pickupJoint = hands.AddComponent<FixedJoint>();
            pickupObject = hit.rigidbody;

            pickupJoint.connectedBody = pickupObject;
        }
    }

    private void DropItem(InputAction.CallbackContext context)
    {
        if (pickupObject == null) return;
        Debug.Log("Drop");
        pickupJoint.connectedBody = null;
        Destroy(pickupJoint);
        pickupObject = null;
    }
    private void ThrowItem(InputAction.CallbackContext context)
    {
        if (pickupObject == null) return;
        Debug.Log("Throw");
        pickupJoint.connectedBody = null;
        Destroy(pickupJoint);
        pickupObject.AddForce(hands.transform.forward * throwForce, ForceMode.Impulse);
        pickupObject = null;
        hands.transform.Rotate(Vector3.zero);
    }

    private void RaiseHands(InputAction.CallbackContext context)
    {
        handRotationValue = context.ReadValue<float>();
    }
    
}

