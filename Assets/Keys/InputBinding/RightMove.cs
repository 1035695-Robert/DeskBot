using UnityEngine;
using UnityEngine.InputSystem;

public class RightMove : KeySlot
{
    protected override void Binding(string keyPath,string keyName)
    {
        InputAction rightAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < rightAction.bindings.Count; i++)
        {
            if (rightAction.bindings[i].isPartOfComposite && rightAction.bindings[i].name == "right")
            {
                rightAction.Disable();
                rightAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{rightAction.bindings[i].name} rebound to {keyPath}");
                UpdateHUD(true);
                rightAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction rightAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < rightAction.bindings.Count; i++)
        {
            if (rightAction.bindings[i].isPartOfComposite && rightAction.bindings[i].name == "right")
            {
                rightAction.Disable();
                rightAction.ApplyBindingOverride(i, "");
                UpdateHUD(false);
                rightAction.Enable();
                break;
            }
        }
    }
}
