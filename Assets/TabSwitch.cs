using UnityEngine;
using UnityEngine.UI;

public class TabSwitch : MonoBehaviour
{
   public Button keyboardButton;
    public Button unlockables;
    
    [SerializeField] private GameObject keyboardUi;
    [SerializeField] private GameObject unlockableUi;
    void OnEnable()
    {
        unlockables.onClick.AddListener(OpenUnlockableAbilityTree);
        keyboardButton.onClick.AddListener(OpenKeyboardUI);
        if(keyboardUi.activeSelf)
            OpenKeyboardUI();
        else if(unlockableUi.activeSelf)
            OpenUnlockableAbilityTree();
    }

    private void OpenKeyboardUI()
    {
        keyboardUi.SetActive(true);
        keyboardButton.interactable = false;
        unlockableUi.SetActive(false);
        unlockables.interactable = true;
    }

    private void OpenUnlockableAbilityTree()
    {
        keyboardUi.SetActive(false);
        keyboardButton.interactable = true;
        unlockableUi.SetActive(true);
        unlockables.interactable = false;
    }
}