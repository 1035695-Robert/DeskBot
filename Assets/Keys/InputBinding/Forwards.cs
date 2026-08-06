using UnityEngine;
using UnityEngine.InputSystem;

public class Forwards : KeySlot
{
    protected override void Binding(string keyPath, string keyName)
    {
       
        InputAction ForwardAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < ForwardAction.bindings.Count; i++)
        {
            if (ForwardAction.bindings[i].isPartOfComposite && ForwardAction.bindings[i].name == "up")
            {
                ForwardAction.Disable();
                ForwardAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{ForwardAction.bindings[i].name} rebound to {keyPath}");
                UpdateHUD(true);
                ForwardAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction ForwardAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < ForwardAction.bindings.Count; i++)
        {
            if (ForwardAction.bindings[i].isPartOfComposite && ForwardAction.bindings[i].name == "up")
            {
                ForwardAction.Disable();
                UpdateHUD(false);
                ForwardAction.ApplyBindingOverride(i, "");
              
                ForwardAction.Enable();
                break;
            }
        }
    }
}

