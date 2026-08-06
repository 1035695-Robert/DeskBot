using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UnlockAblity : MonoBehaviour
{
    private BotAbilities ability;
    public AbilityBundles unlockAbilityBundles;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button unlockButton;
    [SerializeField] private TextMeshProUGUI tokenValueText;

    [Header("key Count")] [SerializeField] private TextMeshProUGUI keyCountText;
    [SerializeField] private int keyCount;

    private void OnEnable()
    {
        EventManager.OnAbilitySelectedEvent += UpdateSelectedAbility;
        EventManager.OnInsertKeyEvent += UpdateKeyTotal;
        unlockButton.onClick.AddListener(Unlock);
        UpdateSelectedAbility(AbilityBundles.Null, string.Empty, 0);
    }

    private void UpdateKeyTotal()
    {
        keyCount++;
        keyCountText.text = keyCount.ToString() + " Keys Obtained";
    }


    private void UpdateSelectedAbility(AbilityBundles abilityName, string description, int abilityPrice)
    {
        unlockAbilityBundles = abilityName;
        nameText.text = unlockAbilityBundles == AbilityBundles.Null ? string.Empty : unlockAbilityBundles.ToString();

        descriptionText.text = description;

        tokenValueText.text = abilityPrice == 0 ? string.Empty : $"{abilityPrice} Keys";

        unlockButton.interactable = BotAbilities.Instance.CanUnlock(abilityName) && abilityName != AbilityBundles.Null && keyCount >= abilityPrice;
    }

    private void Unlock()
    {
        Debug.Log("Test");
        if (unlockAbilityBundles != AbilityBundles.Null)
        {
            BotAbilities.Instance.TryUnlockAbility(unlockAbilityBundles);
        }
    }
}