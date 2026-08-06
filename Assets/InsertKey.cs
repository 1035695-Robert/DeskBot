using System;
using UnityEngine;

public class InsertKey : MonoBehaviour
{
    [SerializeField] private ParticleSystem indication;
    private void OnEnable()
    {
        EventManager.OnHoldingKeyEvent += DisplayIndication;
    }

    private void DisplayIndication()
    {
        indication.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Keys"))
        {
            KeyGenerator.Instance.InsertKey(collision.gameObject);
            collision.gameObject.SetActive(false);
            indication.Stop();
        }
    }
}
