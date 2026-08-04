using UnityEngine;
using UnityEngine.InputSystem;

public class Throw : KeySlot
{
    protected override void Binding(string keyPath, string keyName)
    {
        InputAction throwAction = InputManager.Instance.controls.Player.Throw;

        throwAction.Disable();

        throwAction.ApplyBindingOverride(0, keyPath);
        BindingList.Instance.RemoveFromList(keyName = " = Throw");


        throwAction.Enable();
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction throwAction = InputManager.Instance.controls.Player.Throw;

        throwAction.Disable();

        throwAction.ApplyBindingOverride(0, "");
        BindingList.Instance.RemoveFromList(" Throw");


        throwAction.Enable();
    }
}
