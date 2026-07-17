using UnityEngine;
using UnityEngine.InputSystem;

public class RightRotate : KeyBinding
{
    protected override void Binding(string keyPath)
    {
        InputAction rightRotateAction = InputManager.Instance.Controls.Player.Rotate;
        for (int i = 0; i < rightRotateAction.bindings.Count; i++)
        {
            if (rightRotateAction.bindings[i].isPartOfComposite && rightRotateAction.bindings[i].name == "negative")
            {
                rightRotateAction.Disable();
                rightRotateAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{rightRotateAction.bindings[i].name} rebound to {keyPath}");
                rightRotateAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction rightRotateAction = InputManager.Instance.Controls.Player.Rotate;
        for (int i = 0; i < rightRotateAction.bindings.Count; i++)
        {
            if (rightRotateAction.bindings[i].isPartOfComposite && rightRotateAction.bindings[i].name == "negative")
            {
                rightRotateAction.Disable();
                rightRotateAction.ApplyBindingOverride(i, "");
                rightRotateAction.Enable();
                break;
            }
        }
    }
}