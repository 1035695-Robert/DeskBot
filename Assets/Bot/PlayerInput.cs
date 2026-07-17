using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;


    private Vector2 moveValue;

    private Vector2 moveInput;

    //private Vector3 rotationDirection;
    Rigidbody rb;

    //movement
    InputAction moving;
    InputAction rotateAction;
    InputAction rightRotation;
    bool isMoving, isRotating;

    float rotationDirectionValue;

    //pickUp and Drop
    private Rigidbody pickupObject;
    InputAction pickup;
    InputAction Throw;
    [SerializeField] private float throwForce;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private FixedJoint pickupJoint;

    //HANDS
    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject handView;

    [SerializeField] private float minHandAngle = -90;
    [SerializeField] private float maxHandAngle = 0f;

    private float handRotationValue;
    InputAction handsRaise;
    private float currentHandRotation;


    //horn beep
    private InputAction hornBeep;

    private void Start()
    {
        Debug.Log("BOT");
        moving = InputManager.Instance.Controls.Player.Move;

        rotateAction = InputManager.Instance.Controls.Player.Rotate;


        pickup = InputManager.Instance.Controls.Player.PickUp;
        Throw = InputManager.Instance.Controls.Player.Throw;

        handsRaise = InputManager.Instance.Controls.Player.Hands;

        hornBeep = InputManager.Instance.Controls.Player.Horn;

        rb = GetComponent<Rigidbody>();

        moving.performed += Moving;
        moving.canceled += Moving;

        rotateAction.performed += RotateAction;
        rotateAction.canceled += RotateAction;


        pickup.performed += PickupDrop;
        // dropItem.performed += DropItem;
        Throw.performed += ThrowItem;

        handsRaise.performed += RaiseHands;
        handsRaise.canceled += RaiseHands;

        hornBeep.performed += Beep;
        hornBeep.canceled += Beep;
    }


    private void OnDisable()
    {
        moving.performed -= Moving;
        moving.canceled -= Moving;

        rotateAction.performed -= RotateAction;
        rotateAction.canceled -= RotateAction;

        pickup.performed -= PickupDrop;
        //dropItem.performed -= DropItem;

        handsRaise.performed -= RaiseHands;
        handsRaise.canceled -= RaiseHands;

        hornBeep.performed -= Beep;
        hornBeep.canceled -= Beep;
    }

    private void Update()
    {
        Movement();
        HandAngle();
    }

    #region Movement
    void Movement()
    {
        if (!isRotating && moveValue != Vector2.zero)
        {
            
            Vector3 moveDirection =(transform.forward * moveValue.y) + (transform.right * moveValue.x);
            moveDirection.Normalize();

            Vector3 targetVelocity = moveDirection * moveSpeed;
            rb.linearVelocity = new Vector3(targetVelocity.x, 0, targetVelocity.z);
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
    #endregion
    
    #region pickup
    private void PickupDrop(InputAction.CallbackContext context)
    {
        if (pickupObject != null)
            DropItem();
        else
            Pickup();
    }

    void Pickup()
    {
        if (pickupObject != null) return;
        Debug.Log("Try pickup");

        if (Physics.Raycast(hands.transform.position, hands.transform.forward, out RaycastHit hit, rayDistance,
                PickupLayer))
        {
            Debug.Log("Hit");
            pickupJoint = hands.AddComponent<FixedJoint>();
            pickupObject = hit.rigidbody;

            hit.transform.rotation = hands.transform.rotation;
            hit.transform.position = handView.transform.position + handView.transform.position * 0.01f +
                                     handView.transform.forward * 0.5f;

            pickupJoint.connectedBody = pickupObject;
        }
    }

    private void DropItem()
    {
        if (pickupObject == null) return;
        pickupObject.useGravity = true;
        Debug.Log("Drop");
        pickupJoint.connectedBody = null;
        Destroy(pickupJoint);
        pickupObject.WakeUp();
        handView.transform.localPosition = new Vector3(0, 0, 0.75f);
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
    #endregion
    
    #region Hands
    void HandAngle()
    {
        float nextRotation = currentHandRotation + (handRotationValue * rotationSpeed * 10f) * Time.deltaTime;

        if (nextRotation >= minHandAngle && nextRotation <= maxHandAngle)
        {
            currentHandRotation = nextRotation;
        }
        else if (nextRotation < minHandAngle)
        {
            currentHandRotation = minHandAngle;
        }
        else if (nextRotation > maxHandAngle)
        {
            currentHandRotation = maxHandAngle;
        }

        hands.transform.localRotation = Quaternion.Euler(currentHandRotation, 0, 0);
    }
    private void RaiseHands(InputAction.CallbackContext context)
    {
        handRotationValue = context.ReadValue<float>();
    }
    #endregion
    
    private void Beep(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Beep");
        }

        if (context.canceled)
        {
            Debug.Log("Beep Canceled");
        }
    }
}