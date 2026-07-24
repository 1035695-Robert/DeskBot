using UnityEngine;
using UnityEngine.InputSystem;

public class LeftRotate : KeySlot
{
    protected override void Binding(string keyPath)
    {
        InputAction leftRotateAction = InputManager.Instance.Controls.Player.Rotate;
        for (int i = 0; i < leftRotateAction.bindings.Count; i++)
        {
            if (leftRotateAction.bindings[i].isPartOfComposite && leftRotateAction.bindings[i].name == "positive")
            {
                leftRotateAction.Disable();
                leftRotateAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{leftRotateAction.bindings[i].name} rebound to {keyPath}");
                leftRotateAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction leftRotateAction = InputManager.Instance.Controls.Player.Rotate;
        for (int i = 0; i < leftRotateAction.bindings.Count; i++)
        {
            if (leftRotateAction.bindings[i].isPartOfComposite && leftRotateAction.bindings[i].name == "positive")
            {
                leftRotateAction.Disable();
                leftRotateAction.ApplyBindingOverride(i, "");
                leftRotateAction.Enable();
                break;
            }
        }
    }
}
