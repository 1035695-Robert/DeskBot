using System;
using UnityEngine;
using UnityEngine.UI;

public class TabSwitch : MonoBehaviour
{
   public Button keyboardButton;
    public Button unlockables;
    public Button inventoryButton;
    [SerializeField] private GameObject keyboardUi;
    [SerializeField] private GameObject unlockableUi;
    [SerializeField] private GameObject inventoryUi;

    void OnEnable()
    {
        unlockables.onClick.AddListener(OpenUnlockableAbilityTree);
        keyboardButton.onClick.AddListener(OpenKeyboardUI);
        inventoryButton.onClick.AddListener(OpenKeyInventory);
    }

    private void Start()
    {
        OpenKeyInventory();
    }

    private void OpenKeyboardUI()
    {
        keyboardUi.SetActive(!keyboardUi.activeSelf);
        if (unlockableUi.activeInHierarchy)
            unlockableUi.SetActive(!unlockableUi.activeInHierarchy);
        if (inventoryUi.activeInHierarchy)
            inventoryUi.SetActive(!inventoryUi.activeInHierarchy);
        
    }

    private void OpenUnlockableAbilityTree()
    {
        unlockableUi.SetActive(!unlockableUi.activeSelf);
        if(!keyboardUi.activeInHierarchy)
            keyboardUi.SetActive(!keyboardUi.activeInHierarchy);
    }

    private void OpenKeyInventory()
    {
        inventoryUi.SetActive(!inventoryUi.activeInHierarchy);
        if(!keyboardUi.activeInHierarchy)
            keyboardUi.SetActive(!keyboardUi.activeInHierarchy);

    }
}