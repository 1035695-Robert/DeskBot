using UnityEngine;
using UnityEngine.InputSystem;

public class Throw : KeySlot
{
    protected override void Binding(string keyPath, string keyName)
    {
        InputAction throwAction = InputManager.Instance.controls.Player.Throw;

        throwAction.Disable();

        throwAction.ApplyBindingOverride(0, keyPath);
        UpdateHUD(true);


        throwAction.Enable();
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction throwAction = InputManager.Instance.controls.Player.Throw;

        throwAction.Disable();

        throwAction.ApplyBindingOverride(0, "");
        UpdateHUD(false);

        throwAction.Enable();
    }
}