using System;


public class EventManager
{
    public delegate void OnAudioRequest(string clipName);

    public static OnAudioRequest OnAudioRequestEvent;
    public static Action OnAudioCancelEvent;
   
}
