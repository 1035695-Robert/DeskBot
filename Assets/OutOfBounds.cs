using System;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
   // gameobject = boxSpawnZone

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.position = Vector3.zero;
        }
        // else if (collision.transform.CompareTag("Pickup"))
        // {
        //     
        // }
    }
}
