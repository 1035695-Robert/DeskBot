using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public enum AbilityList
{
    Forwards,
    Backwards,
    RotateLeft,
    RotateRight,
    Left,
    Right,
    RaiseHands,
    LowerHands,
    Horn,
    GrabDrop,
    Throw
}
public abstract class KeySlot : MonoBehaviour, IKeyDropSlot
{
    [Header("KeyBinding variables")] [SerializeField]
    string bindedKey;
    
    [SerializeField] private GameObject currentKey;
    [SerializeField] private GameObject previousKey;
    [SerializeField] private AbilityList ability;
    private string currentKeyName;
    
    public void OnKeyDrop(KeyDrag key, string keyName)
    {
        key.transform.position = transform.position;
        Debug.Log("Binding");
        if (currentKey != null)
        {
            previousKey = currentKey;
        }

        currentKey = key.gameObject;
        currentKey.transform.SetParent(transform);
        if (previousKey != null)
        {
            IRemoveKey removeKey = previousKey.GetComponent<IRemoveKey>();
            removeKey.OnKeyRemoval(previousKey);
            previousKey = null;
        }
        
        currentKeyName = keyName.TrimEnd();
        string keyPath = $"<keyboard>/{keyName.ToLower()}";
        Binding(keyPath, currentKeyName);
    }

    protected void UpdateHUD(bool state)
    {
        if(state)
            BindingList.Instance.AddToList(currentKeyName, ability);
        else
            BindingList.Instance.RemoveFromList(ability);
    }
    

    protected abstract void Binding(string keyPath, string keyName);

    public virtual void OnNullifyBind()
    {
        currentKey = null;
    }
}