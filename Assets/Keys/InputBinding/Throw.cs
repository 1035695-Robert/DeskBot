using UnityEngine;
using UnityEngine.InputSystem;

public class Throw : KeyBinding
{
    protected override void Binding(string keyPath)
    {
        InputAction RightRotateAction = InputManager.Instance.Controls.Player.Throw;

        RightRotateAction.Disable();

        RightRotateAction.ApplyBindingOverride(0, keyPath);

        RightRotateAction.Enable();
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction RightRotateAction = InputManager.Instance.Controls.Player.Throw;

        RightRotateAction.Disable();

        RightRotateAction.ApplyBindingOverride(0, "");

        RightRotateAction.Enable();
    }
}
