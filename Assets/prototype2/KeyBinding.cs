using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class KeyBinding : MonoBehaviour, IKeyDropSlot
{

    [SerializeField] string bindedKey;

    public void OnKeyDrop(KeyDrag key, GameObject keyObject)
    {
        key.transform.position = transform.position;
        Debug.Log("BInding");

        bindedKey = keyObject.name;
        string keyPath = $"<keyboard>/{bindedKey.ToLower()}";
        Binding(keyPath);
    }
    public abstract void Binding(string keyPath);
   

}
