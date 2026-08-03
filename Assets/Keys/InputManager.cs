using UnityEngine;

public class InputManager
{
    public static readonly InputManager Instance;
    public readonly BotInputs controls;

    static InputManager()
    {
        Instance = new InputManager();
    }

    private InputManager()
    {
        controls = new BotInputs();
        controls.Enable();
    }
}