using UnityEngine;

public interface IKeyDropSlot 
{
    void OnKeyDrop(KeyDrag key, string keyBindingName);
    void OnNullifyBind();
}

public interface IRemoveKey
{
    void OnKeyRemoval(GameObject key);
}

public interface IUnlockAbility
{
    void OnUnlockAbility();
}
