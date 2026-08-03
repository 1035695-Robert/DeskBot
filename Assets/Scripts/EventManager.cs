

public class EventManager
{
    public delegate void OnAudioRequest(string clipName);

    public static OnAudioRequest OnAudioRequestEvent;
    


    public delegate void OnEnterKeyboardArea(bool state);
    public static OnEnterKeyboardArea OnEnterKeyboardAreaEvent;
    
    public delegate void OnTaskCompletion();
    public static OnTaskCompletion OnTaskCompletionEvent;

    public delegate void OnAbilitySelected(AbilityType name, string description, string price);
    public static OnAbilitySelected OnAbilitySelectedEvent;

}
