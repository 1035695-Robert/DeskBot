using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class KeySlot : MonoBehaviour, IKeyDropSlot
{
    [Header("KeyBinding variables")] [SerializeField]
    string bindedKey;

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

        keyName.TrimEnd();
        string keyPath = $"<keyboard>/{keyName.ToLower()}";
        Binding(keyPath);
    }


    protected abstract void Binding(string keyPath);

    public virtual void OnNullifyBind()
    {
        currentKey = null;
    }
}