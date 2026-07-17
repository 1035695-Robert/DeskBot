using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public InputManager instance;
    public BotInputs Controls;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Instance");
            Instance = this;
            instance = Instance;
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

