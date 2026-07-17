using UnityEngine;
using UnityEngine.InputSystem;

public class Forwards : KeyBinding
{
    protected override void Binding(string keyPath)
    {
       
        InputAction ForwardAction = InputManager.Instance.Controls.Player.Move;
        for (int i = 0; i < ForwardAction.bindings.Count; i++)
        {
            if (ForwardAction.bindings[i].isPartOfComposite && ForwardAction.bindings[i].name == "up")
            {
                ForwardAction.Disable();
                ForwardAction.ApplyBindingOverride(i, keyPath);
                Debug.Log($"{ForwardAction.bindings[i].name} rebound to {keyPath}");
                ForwardAction.Enable();
                break;
            }
        }
    }

    public override void OnNullifyBind()
    {
        base.OnNullifyBind();
        InputAction ForwardAction = InputManager.Instance.Controls.Player.Move;
        for (int i = 0; i < ForwardAction.bindings.Count; i++)
        {
            if (ForwardAction.bindings[i].isPartOfComposite && ForwardAction.bindings[i].name == "up")
            {
                ForwardAction.Disable();

                ForwardAction.ApplyBindingOverride(i, "");
              
                ForwardAction.Enable();
                break;
            }
        }
    }
}

