using UnityEngine;

public class SetKeyBinding : MonoBehaviour
{
    public void OnKeyDrop(GameObject key)
    {
        key.transform.position = transform.position;
        Debug.Log("bind Key");
    }

}
