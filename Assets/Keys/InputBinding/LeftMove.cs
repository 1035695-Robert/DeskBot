using UnityEngine;
using UnityEngine.InputSystem;

public class LeftMove : KeySlot
{
    protected override void Binding(string keyPath, string keyName)
    {
        InputAction leftAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < leftAction.bindings.Count; i++)
        {
            if (leftAction.bindings[i].isPartOfComposite && leftAction.bindings[i].name == "left")
            {
                leftAction.Disable();
                leftAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{leftAction.bindings[i].name} rebound to {keyPath}");
                BindingList.Instance.AddToList(keyName + " = Left Movement");

                leftAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction leftAction = InputManager.Instance.controls.Player.Move;
        for (int i = 0; i < leftAction.bindings.Count; i++)
        {
            if (leftAction.bindings[i].isPartOfComposite && leftAction.bindings[i].name == "left")
            {
                leftAction.Disable();
                leftAction.ApplyBindingOverride(i, "");
                BindingList.Instance.RemoveFromList("Left Movement");

                leftAction.Enable();
                break;
            }
        }
    }
}
