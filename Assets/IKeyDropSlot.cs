using UnityEngine;

public interface IKeyDropSlot 
{
    void OnKeyDrop(KeyDrag key, string keyBindingName);
    void OnNullifyBind();
}

