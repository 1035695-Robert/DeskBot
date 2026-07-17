using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class KeyBinding : MonoBehaviour, IKeyDropSlot
{

    [SerializeField] string bindedKey;
    [SerializeField] private GameObject currentKey;
    [SerializeField] private GameObject previousKey;

    public void OnKeyDrop(KeyDrag key, string keyName)
    {
        key.transform.position = transform.position;
        Debug.Log("Binding");
        if (currentKey != null)
        {
            previousKey = currentKey;
        }
        currentKey = key.gameObject;
        currentKey.transform.SetParent(transform.root);
        if (previousKey != null)
        {
            IRemoveKey removeKey = previousKey.GetComponent<IRemoveKey>();
            removeKey.OnKeyRemoval(previousKey);
            previousKey = null;
        }
        
        string keyPath = $"<keyboard>/{keyName.ToLower()}";
        Binding(keyPath);
    }


    protected abstract void Binding(string keyPath);

    public virtual void OnNullifyBind()
    {
        currentKey = null;
    }
    
}
