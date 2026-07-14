using UnityEngine;
using UnityEngine.InputSystem;

public class HandsLower : KeyBinding
{    public override void Binding(string keyPath)
    {
        InputAction LowerHandAction = InputManager.Instance.Controls.Player.Hands;
        for (int i = 0; i < LowerHandAction.bindings.Count; i++)
        {
            if (LowerHandAction.bindings[i].isPartOfComposite && LowerHandAction.bindings[i].name == "positive")
            {
                LowerHandAction.Disable();
                LowerHandAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{LowerHandAction.bindings[i].name} rebound to {keyPath}");
                LowerHandAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        InputAction LowerHandAction = InputManager.Instance.Controls.Player.Hands;
        for (int i = 0; i < LowerHandAction.bindings.Count; i++)
        {
            if (LowerHandAction.bindings[i].isPartOfComposite && LowerHandAction.bindings[i].name == "positive")
            {
                LowerHandAction.Disable();
                LowerHandAction.ApplyBindingOverride(i, "");
                LowerHandAction.Enable();
                break;
            }
        }
    }
}
