using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    private Vector2 moveInput;

    //private Vector3 rotationDirection;
  

    //movement
    
    [SerializeField] private float handsRotationSpeed;


    Movement movementScript;


    //pickUp and Drop
    private Rigidbody pickupObject;
    InputAction pickup;
    InputAction Throw;
    [SerializeField] private float throwForce;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask pickupLayer;
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
        

      
        
        pickup = InputManager.Instance.Controls.Player.PickUp;
        Throw = InputManager.Instance.Controls.Player.Throw;

        handsRaise = InputManager.Instance.Controls.Player.Hands;

        hornBeep = InputManager.Instance.Controls.Player.Horn;

        

        pickup.performed += PickupDrop;
        Throw.performed += ThrowItem;

        handsRaise.performed += RaiseHands;
        handsRaise.canceled += RaiseHands;

        hornBeep.performed += Beep;
       
    }


    private void OnDisable()
    {
        pickup.performed -= PickupDrop;
        Throw.performed -= ThrowItem;
        
        handsRaise.performed -= RaiseHands;
        handsRaise.canceled -= RaiseHands;

        hornBeep.performed -= Beep;
    }

    private void Update()
    {
        HandAngle();
    }

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
                pickupLayer))
        {
            Debug.Log("Hit");
            pickupJoint = hands.AddComponent<FixedJoint>();
            pickupObject = hit.rigidbody;
            
            EventManager.OnAudioRequestEvent?.Invoke("Pickup");
            
            hit.transform.rotation = hands.transform.rotation;
            hit.transform.position = handView.transform.position
                                     + handView.transform.position * 0.01f
                                     + handView.transform.forward * 0.5f
                                     + handView.transform.up * 0.2f;
            
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
        EventManager.OnAudioRequestEvent?.Invoke("Drop");
        pickupObject.WakeUp();
        // handView.transform.localPosition = new Vector3(0, , 0.75f);
        pickupObject = null;
    }

    private void ThrowItem(InputAction.CallbackContext context)
    {
        if (pickupObject == null) return;
        Debug.Log("Throw");
        pickupJoint.connectedBody = null;
        Destroy(pickupJoint);
        pickupObject.AddForce(hands.transform.forward * throwForce, ForceMode.Impulse);
        
        EventManager.OnAudioRequestEvent?.Invoke("Throw");
        
        pickupObject = null;
        hands.transform.Rotate(Vector3.zero);
    }

    #endregion

    #region Hands

    void HandAngle()
    {
        float nextRotation = currentHandRotation + (handRotationValue * handsRotationSpeed * 10f) * Time.deltaTime;

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
            EventManager.OnAudioRequestEvent?.Invoke("BeepTune");
        }
    }
}