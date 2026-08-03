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
        if(!keyboardUi.activeSelf)
            OpenKeyboardUI();
        else if(unlockableUi.activeSelf)
            OpenUnlockableAbilityTree();
    }

    private void OpenKeyboardUI()
    {
        keyboardUi.SetActive(!keyboardUi.activeSelf);
        if (unlockableUi.activeInHierarchy)
            unlockableUi.SetActive(!unlockableUi.activeInHierarchy);
        
    }

    private void OpenUnlockableAbilityTree()
    {
        unlockableUi.SetActive(!unlockableUi.activeSelf);
        if(!keyboardUi.activeInHierarchy)
            keyboardUi.SetActive(!keyboardUi.activeInHierarchy);
    }
}