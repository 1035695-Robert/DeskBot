using UnityEngine;
using UnityEngine.InputSystem;

public class Backwards : KeySlot
{
    protected override void Binding(string keyPath , string keyName)
    {
        InputAction BackwardsAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < BackwardsAction.bindings.Count; i++)
        {
            if (BackwardsAction.bindings[i].isPartOfComposite && BackwardsAction.bindings[i].name == "down")
            {
                BackwardsAction.Disable();
                BackwardsAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{BackwardsAction.bindings[i].name} rebound to {keyPath}");
                BindingList.Instance.AddToList( keyPath + " = Backwards");

                BackwardsAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction BackwardsAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < BackwardsAction.bindings.Count; i++)
        {
            if (BackwardsAction.bindings[i].isPartOfComposite && BackwardsAction.bindings[i].name == "down")
            {
                BackwardsAction.Disable();
                BackwardsAction.ApplyBindingOverride(i, "");
                BindingList.Instance.RemoveFromList(" = Backwards");

                BackwardsAction.Enable();
                break;
            }
        }
    }
}
