using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    Null,
    Forwards,
    GrabDrop,
    Backwards,
    MoveLeft,
    MoveRight,
    Throw,
    RotateLeft,
    RotateRight,
    RaiseHands,
    LowerHands,
    Horn,
}

public class BotAbilities
{
    private static BotAbilities _instance;

    public static BotAbilities Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BotAbilities();
            return _instance;
        }
    }

    private List<AbilityType> unlockedAbilityTypeList = new List<AbilityType>();
    // = new List<AbilityType>
    // {
    //     AbilityType.Forwards, AbilityType.GrabDrop, AbilityType.RotateLeft,
    // };

    private void UnlockAbility(AbilityType ability)
    {
        if (!IsAbilityUnlocked(ability))
        {
            unlockedAbilityTypeList.Add(ability);
            if (GameObject.Find(ability.ToString()).TryGetComponent(out IUnlockAbility abilityUnlock))
                abilityUnlock.OnUnlockAbility();
            else Debug.LogError("no unlock ability found");
        }
    }

    public bool IsAbilityUnlocked(AbilityType ability)
    {
        return unlockedAbilityTypeList.Contains(ability);
    }

    private AbilityType Requirement(AbilityType ability)
    {
        switch (ability)
        {
            case AbilityType.MoveLeft: return AbilityType.RotateLeft;
            case AbilityType.MoveRight: return AbilityType.RotateRight;
        }

        return AbilityType.Null;
    }

    public bool TryUnlockAbility(AbilityType ability)
    {
        AbilityType abilityRequirement = Requirement(ability);
        if (abilityRequirement != AbilityType.Null)
        {
            if (IsAbilityUnlocked(abilityRequirement))
            {
                UnlockAbility(ability);
                return true;
            }
            return false;
        }

        UnlockAbility(ability);
        return true;
    }

    public bool CanUnlock(AbilityType ability)
    {
        AbilityType abilityRequirement = Requirement(ability);
        if (abilityRequirement != AbilityType.Null)
            if (IsAbilityUnlocked(abilityRequirement))
                return true;
            else return false;
        UnlockAbility(ability);
        return true;
    }
}