using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public BotInputs Controls;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Controls = new BotInputs();
            Controls.Enable();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

