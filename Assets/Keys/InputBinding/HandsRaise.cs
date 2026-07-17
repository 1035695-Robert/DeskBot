using UnityEngine;
using UnityEngine.InputSystem;

public class HandsRaise : KeyBinding
{
    protected override void Binding(string keyPath)
    {
        InputAction RaiseHandAction = InputManager.Instance.Controls.Player.Hands;
        for (int i = 0; i < RaiseHandAction.bindings.Count; i++)
        {
            if (RaiseHandAction.bindings[i].isPartOfComposite && RaiseHandAction.bindings[i].name == "negative")
            {
                RaiseHandAction.Disable();
                RaiseHandAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{RaiseHandAction.bindings[i].name} rebound to {keyPath}");
                RaiseHandAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction RaiseHandAction = InputManager.Instance.Controls.Player.Hands;
        for (int i = 0; i < RaiseHandAction.bindings.Count; i++)
        {
            if (RaiseHandAction.bindings[i].isPartOfComposite && RaiseHandAction.bindings[i].name == "negative")
            {
                RaiseHandAction.Disable();
                RaiseHandAction.ApplyBindingOverride(i, "");
                RaiseHandAction.Enable();
                break;
            }
        }
    }
}
