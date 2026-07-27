using UnityEngine;


public class Test : MonoBehaviour
{
    private BotAbilities ability;
    public AbilityType unlockAbilityType;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Test");
            if(unlockAbilityType != AbilityType.Null)
                if(BotAbilities.Instance.TryUnlockAbility(unlockAbilityType));
        }
    }
}
