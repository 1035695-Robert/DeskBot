using Keys;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Test : MonoBehaviour
{
    private BotAbilities ability;
    public AbilityType unlockAbilityType;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Test");
            if(BotAbilities.Instance.TryUnlockAbility(unlockAbilityType));
        }
    }
}
