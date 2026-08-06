using System;
using UnityEngine;

public class DropBox : MonoBehaviour
{
   [SerializeField] DropZoneCheck dropZoneCheck;

    [SerializeField] HitColour hitColour;
    Renderer targetRenderer;

    private void Start()
    {
        dropZoneCheck = transform.root.GetComponent<DropZoneCheck>();
        targetRenderer = transform.GetComponentInChildren<Renderer>();
        switch (hitColour)
        {
            case HitColour.Blue:
                targetRenderer.material.color = Color.blue;
                break;
            case HitColour.Green:
                targetRenderer.material.color = Color.green;
                break;
            case HitColour.Yellow:
                targetRenderer.material.color = Color.yellow;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Box box = collision.transform.GetComponent<Box>();
        if (box != null)
        {
            if (box.colour == hitColour)
            {
                dropZoneCheck.onBoxPlacementEvent?.Invoke();
                Debug.Log("Box");
            }
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        Box box = collision.transform.GetComponent<Box>();
        if (box != null)
        {
            if (box.colour == hitColour)
            {
                dropZoneCheck.onBoxReplacementEvent?.Invoke();
            }
        }
    }
}