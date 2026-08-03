using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    Null,
    BaseControls,
    SideMovement,
    HandsBundle,
}

public class BotAbilities
{
    // public delegate void OnUnlockAbility(AbilityType ability);
    // public static OnUnlockAbility onUnlockAbility;
    
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

    private List<AbilityType> unlockedAbilityTypeList = new List<AbilityType>
    {
       AbilityType.BaseControls
    };

    private void UnlockAbility(AbilityType ability)
    {
        if (!IsAbilityUnlocked(ability))
        {
            unlockedAbilityTypeList.Add(ability);
            string[] ablilityArray = SelectAbilityInBundle(ability);
            foreach (string abilityName in ablilityArray)
            {
                if (GameObject.Find(abilityName).TryGetComponent(out IUnlockAbility abilityUnlock))
                    abilityUnlock.OnUnlockAbility();
                else
                    Debug.LogError("Couldn't find " + abilityName);
            }
        }
    }

    private string[] SelectAbilityInBundle(AbilityType ability)
    {
        List<string> abilityList = new List<string>();
        switch (ability)
        {
            case AbilityType.SideMovement:
                abilityList.Add("Backwards");
                abilityList.Add("MoveLeft");
                abilityList.Add("MoveRight");
                return abilityList.ToArray();
            case AbilityType.HandsBundle:
                abilityList.Add("RaiseHands");
                abilityList.Add("LowerHands");
                abilityList.Add("Throw");
                return abilityList.ToArray();
        }
        return abilityList.ToArray();
    }

    public bool IsAbilityUnlocked(AbilityType ability)
    {
        return unlockedAbilityTypeList.Contains(ability);
    }

    private AbilityType Requirement(AbilityType ability)
    {
        switch (ability)
        {
            case AbilityType.SideMovement: return AbilityType.HandsBundle;
            case AbilityType.HandsBundle: return AbilityType.BaseControls;
        }
        
        return AbilityType.Null;
    }

    public void TryUnlockAbility(AbilityType ability)
    {
        if (CanUnlock(ability))
        {
            UnlockAbility(ability);
        }
    }

    public bool CanUnlock(AbilityType ability)
    {
        AbilityType abilityRequirement = Requirement(ability);
        if (abilityRequirement != AbilityType.Null)
        {
            if (!IsAbilityUnlocked(abilityRequirement))
            {
                return false;
            }
        }
        return true;
    }
}