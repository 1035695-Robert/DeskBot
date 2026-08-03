using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelection : MonoBehaviour
{
    public AbilityType abilityName;
    [TextArea(3, 10)] public string abilityDescription;
    [SerializeField] Button selectAbilityButton;
    [SerializeField] private int abilityPrice;
    private void OnEnable()
    {
        selectAbilityButton.onClick.AddListener(OnSelected);
    }
    
    private void OnSelected()
    {
      EventManager.OnAbilitySelectedEvent?.Invoke(abilityName, abilityDescription, abilityPrice.ToString());
    }

    private void OnDisable()
    {
        selectAbilityButton.onClick.RemoveAllListeners();
    }
}
