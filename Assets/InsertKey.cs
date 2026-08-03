using UnityEngine;

public class InsertKey : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Keys"))
        {
            KeyGenerator.Instance.InsertKey(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
