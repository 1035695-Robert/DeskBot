using System;
using UnityEngine;
using UnityEngine.EventSystems;



public class KeyDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IRemoveKey
{


    private Vector3 startPosition;
    [SerializeField] private GameObject inventory;
    private Collider2D col;
    public GameObject currentSlot;
    public GameObject previousSlot;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        inventory = GameObject.Find("key inventory");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Click");
        transform.SetParent(transform.root, false);
        startPosition = transform.position;
        transform.position = Input.mousePosition;
        if (currentSlot != null)
        {
            previousSlot = currentSlot;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);

        if (hitCollider != null && hitCollider.TryGetComponent(out IKeyDropSlot keyDropSlot))
        {
            currentSlot = hitCollider.gameObject;   
            if(previousSlot != null && previousSlot.TryGetComponent(out IKeyDropSlot RemoveBnding))
            {
                RemoveBnding.OnNullifyBind();
                previousSlot = null;
                
            }
            keyDropSlot.OnKeyDrop(this, gameObject.name);
        }
        else
        {
            OnKeyRemoval(gameObject);
        }
    }


    public void OnKeyRemoval(GameObject key)
    {
        key.transform.SetParent(inventory.transform);
        currentSlot = null;
       
    }
}
