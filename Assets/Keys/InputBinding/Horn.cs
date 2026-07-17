using UnityEngine;
using UnityEngine.InputSystem;

public class Horn : KeyBinding
{
    protected override void Binding(string keyPath)
    {
        InputAction hornAction = InputManager.Instance.Controls.Player.Horn;

        hornAction.Disable();

        hornAction.ApplyBindingOverride(0, keyPath);

        hornAction.Enable();
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction hornAction = InputManager.Instance.Controls.Player.Horn;

        hornAction.Disable();

        hornAction.ApplyBindingOverride(0, "");

        hornAction.Enable();
    }
}
