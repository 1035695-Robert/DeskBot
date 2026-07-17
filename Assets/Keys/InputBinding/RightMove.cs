using UnityEngine;
using UnityEngine.InputSystem;

public class RightMove : KeyBinding
{
    protected override void Binding(string keyPath)
    {
        InputAction rightAction = InputManager.Instance.Controls.Player.Move;
        for (int i = 0; i < rightAction.bindings.Count; i++)
        {
            if (rightAction.bindings[i].isPartOfComposite && rightAction.bindings[i].name == "right")
            {
                rightAction.Disable();
                rightAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{rightAction.bindings[i].name} rebound to {keyPath}");
                rightAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction rigthAction = InputManager.Instance.Controls.Player.Move;
        for (int i = 0; i < rigthAction.bindings.Count; i++)
        {
            if (rigthAction.bindings[i].isPartOfComposite && rigthAction.bindings[i].name == "right")
            {
                rigthAction.Disable();
                rigthAction.ApplyBindingOverride(i, "");
                rigthAction.Enable();
                break;
            }
        }
    }
}
