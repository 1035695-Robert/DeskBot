

public class EventManager
{
    public delegate void OnAudioRequest(string clipName);

    public static OnAudioRequest OnAudioRequestEvent;

    public delegate void OnHoldingKey();
    public static OnHoldingKey OnHoldingKeyEvent;

    public delegate void OnEnterKeyboardArea(bool state);
    public static OnEnterKeyboardArea OnEnterKeyboardAreaEvent;
    
    public delegate void OnTaskCompletion();
    public static OnTaskCompletion OnTaskCompletionEvent;

    public delegate void OnAbilitySelected(AbilityBundles name, string description, int price);
    public static OnAbilitySelected OnAbilitySelectedEvent;

    public delegate void OnInsertKey();
    public static OnInsertKey OnInsertKeyEvent;
}
