using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : KeyBinding
{
    public override void Binding(string keyPath)
    {
        InputAction LeftRotateAction = InputManager.Instance.Controls.Player.PickUp;

        LeftRotateAction.Disable();

        LeftRotateAction.ApplyBindingOverride(0, keyPath);

        LeftRotateAction.Enable();

    }

    public override void OnNullifyBind()
    {
        InputAction LeftRotateAction = InputManager.Instance.Controls.Player.PickUp;

        LeftRotateAction.Disable();

        LeftRotateAction.ApplyBindingOverride(0, "");

        LeftRotateAction.Enable();
    }
}
