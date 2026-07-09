using UnityEngine;
using UnityEngine.InputSystem;

public class LeftRotate : KeyBinding
{
    public override void Binding(string keyPath)
    {
        InputAction LeftRotateAction = InputManager.Instance.Controls.Player.LeftRotate;

        LeftRotateAction.Disable();

        LeftRotateAction.ApplyBindingOverride(0, keyPath);

        LeftRotateAction.Enable();
    }

    public override void OnNullifyBind()
    {
        InputAction LeftRotateAction = InputManager.Instance.Controls.Player.LeftRotate;

        LeftRotateAction.Disable();

        LeftRotateAction.ApplyBindingOverride(0, "");

        LeftRotateAction.Enable();
    }
}
