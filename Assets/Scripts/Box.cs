using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    public HitColour colour;
    Vector3 startPosition;
    private Rigidbody rb;

    private void Awake()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        ResetBox();
    }

    public void ResetBox()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}