using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : KeySlot
{
    protected override void Binding(string keyPath, string keyName)
    {
        InputAction pickUpAction = InputManager.Instance.controls.Player.PickUp;

        pickUpAction.Disable();

        pickUpAction.ApplyBindingOverride(0, keyPath);
        UpdateHUD(true);

        pickUpAction.Enable();

    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        
        InputAction pickupAction = InputManager.Instance.controls.Player.PickUp;

        pickupAction.Disable();

        pickupAction.ApplyBindingOverride(0, "");
        UpdateHUD(false);
        pickupAction.Enable();
    }
}
