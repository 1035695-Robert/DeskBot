using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class KeyBinding : MonoBehaviour, IKeyDropSlot
{

    [SerializeField] string bindedKey;

    public void OnKeyDrop(KeyDrag key, string keyName)
    {
        key.transform.position = transform.position;
        Debug.Log("BInding");

        
        string keyPath = $"<keyboard>/{keyName.ToLower()}";
        Binding(keyPath);
    }


    public abstract void Binding(string keyPath);

    public abstract void OnNullifyBind();
    
}
