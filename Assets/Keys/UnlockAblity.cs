using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UnlockAblity : MonoBehaviour
{
    private BotAbilities ability;
    public AbilityType unlockAbilityType;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button unlockButton;
    [SerializeField] private TextMeshProUGUI tokenValueText;
    
    private void OnEnable()
    {
        EventManager.OnAbilitySelectedEvent += UpdateSelectedAbility;
        unlockButton.onClick.AddListener(Unlock);
    }

    

    private void UpdateSelectedAbility(AbilityType abilityName, string description, string abilityPrice)
    {
      unlockAbilityType = abilityName;
      nameText.text = unlockAbilityType.ToString();
      descriptionText.text = description;
      tokenValueText.text = abilityPrice.ToString() + "Keys";
      if(!BotAbilities.Instance.CanUnlock(abilityName))
          unlockButton.interactable = false;
      else
          unlockButton.interactable = true;
      
    }

    private void Unlock()
    {
            Debug.Log("Test");
            if(unlockAbilityType != AbilityType.Null)
            {
                BotAbilities.Instance.TryUnlockAbility(unlockAbilityType);
            }
        
    }
}
