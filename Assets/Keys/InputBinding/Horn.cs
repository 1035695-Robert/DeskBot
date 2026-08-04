using UnityEngine;
using UnityEngine.InputSystem;

public class Horn : KeySlot
{
    protected override void Binding(string keyPath,string keyName)
    {
        InputAction hornAction = InputManager.Instance.controls.Player.Horn;

        hornAction.Disable();

        hornAction.ApplyBindingOverride(0, keyPath);
        BindingList.Instance.AddToList(keyName + " = Horn");


        hornAction.Enable();
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction hornAction = InputManager.Instance.controls.Player.Horn;

        hornAction.Disable();

        hornAction.ApplyBindingOverride(0, "");
        BindingList.Instance.RemoveFromList(" = Horn");


        hornAction.Enable();
    }
}
