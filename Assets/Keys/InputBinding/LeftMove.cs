using UnityEngine;
using UnityEngine.InputSystem;

public class LeftMove : KeySlot
{
    protected override void Binding(string keyPath, string keyName)
    {
        InputAction leftAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < leftAction.bindings.Count; i++)
        {
            if (leftAction.bindings[i].isPartOfComposite && leftAction.bindings[i].name == "left")
            {
                leftAction.Disable();
                leftAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{leftAction.bindings[i].name} rebound to {keyPath}");
                UpdateHUD(true);
                leftAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction leftAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < leftAction.bindings.Count; i++)
        {
            if (leftAction.bindings[i].isPartOfComposite && leftAction.bindings[i].name == "left")
            {
                leftAction.Disable();
                leftAction.ApplyBindingOverride(i, "");
                UpdateHUD(false);
                leftAction.Enable();
                break;
            }
        }
    }
}
