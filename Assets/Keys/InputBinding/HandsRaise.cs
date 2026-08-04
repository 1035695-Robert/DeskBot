using UnityEngine;
using UnityEngine.InputSystem;

public class HandsRaise : KeySlot
{
    protected override void Binding(string keyPath,  string keyName)
    {
        InputAction RaiseHandAction = InputManager.Instance.controls.Player.Hands;
        for (int i = 0; i < RaiseHandAction.bindings.Count; i++)
        {
            if (RaiseHandAction.bindings[i].isPartOfComposite && RaiseHandAction.bindings[i].name == "negative")
            {
                RaiseHandAction.Disable();
                RaiseHandAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{RaiseHandAction.bindings[i].name} rebound to {keyPath}");
                BindingList.Instance.AddToList(keyName +" + Raise Hands");

                RaiseHandAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction RaiseHandAction = InputManager.Instance.controls.Player.Hands;
        for (int i = 0; i < RaiseHandAction.bindings.Count; i++)
        {
            if (RaiseHandAction.bindings[i].isPartOfComposite && RaiseHandAction.bindings[i].name == "negative")
            {
                RaiseHandAction.Disable();
                RaiseHandAction.ApplyBindingOverride(i, "");
                BindingList.Instance.RemoveFromList("Raise Hands");

                RaiseHandAction.Enable();
                break;
            }
        }
    }
}
