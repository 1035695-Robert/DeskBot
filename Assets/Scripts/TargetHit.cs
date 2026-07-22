
using UnityEngine;


public enum HitColour
{
    Yellow,
    Blue,
    Green
}
public class TargetHit : MonoBehaviour
{
    [SerializeField] HitColour hitColour;

    void Start()
    {
        ChangeTargetColour();
    }
    private void OnTriggerEnter(Collider other)
    { 
        Box box = other.GetComponent<Box>();
        if (box.hitColour == hitColour)
        {
            box.ResetBox();
            ChangeTargetColour();
        }
    }

    private void ChangeTargetColour()
    {
       hitColour = (HitColour)Random.Range(0, 3);
       switch (hitColour)
       {
           case HitColour.Blue:
               GetComponent<Renderer>().material.color = Color.blue;
               break;
           case HitColour.Green:
               GetComponent<Renderer>().material.color = Color.green;
               break;
           case HitColour.Yellow:
               GetComponent<Renderer>().material.color = Color.yellow;
               break;
       }
    }
}
