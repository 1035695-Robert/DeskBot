using System;
using UnityEngine;
using UnityEngine.EventSystems;



public class KeyDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{


    private Vector3 startPosition;
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Click");
        startPosition = transform.position;
        transform.position = Input.mousePosition;
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
            keyDropSlot.OnKeyDrop(this, gameObject);
        }
        else
        {
            transform.position = startPosition;
        }
    }
}
