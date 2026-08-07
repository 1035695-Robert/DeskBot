using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum AbilityBundles
{
    Null,
    BaseControls,
    MovementUpgrade,
    HandsUpgrade,
    Mystery
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

    private List<AbilityBundles> unlockedAbilityTypeList = new List<AbilityBundles>
    {
       AbilityBundles.BaseControls
    };

    private void UnlockAbility(AbilityBundles ability)
    {
        if (!IsAbilityUnlocked(ability))
        {
            if (ability == AbilityBundles.Mystery)
            {
                Debug.Log("mysteryunlock");
                EventManager.OnMysteryBundleEvent?.Invoke();
                return;
            }
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

    private string[] SelectAbilityInBundle(AbilityBundles ability)
    {
        List<string> abilityList = new List<string>();
        switch (ability)
        {
            case AbilityBundles.MovementUpgrade:
                abilityList.Add("Backwards");
                abilityList.Add("MoveLeft");
                abilityList.Add("MoveRight");
                return abilityList.ToArray();
            case AbilityBundles.HandsUpgrade:
                abilityList.Add("RaiseHands");
                abilityList.Add("LowerHands");
                abilityList.Add("Throw");
                return abilityList.ToArray();
        }
        return abilityList.ToArray();
    }

    public bool IsAbilityUnlocked(AbilityBundles ability)
    {
        return unlockedAbilityTypeList.Contains(ability);
    }

    private AbilityBundles Requirement(AbilityBundles ability)
    {
        switch (ability)
        {
            case AbilityBundles.HandsUpgrade: return AbilityBundles.BaseControls;
            case AbilityBundles.MovementUpgrade: return AbilityBundles.HandsUpgrade;
            case  AbilityBundles.Mystery: return AbilityBundles.MovementUpgrade;
        }
        
        return AbilityBundles.Null;
    }

    public void TryUnlockAbility(AbilityBundles ability)
    {
        if (CanUnlock(ability))
        {
            UnlockAbility(ability);
        }
    }

    public bool CanUnlock(AbilityBundles ability)
    {
        AbilityBundles abilityRequirement = Requirement(ability);
        if (abilityRequirement != AbilityBundles.Null)
        {
            if (!IsAbilityUnlocked(abilityRequirement))
            {
                return false;
            }
        }
        return true;
    }
}