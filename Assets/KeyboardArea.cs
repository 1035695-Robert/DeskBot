using System;
using UnityEngine;

public class KeyboardArea : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUi;
    [SerializeField] private GameObject tabsUI;

    public bool isActiveState;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            EventManager.OnEnterKeyboardAreaEvent?.Invoke(true);
            UiSwitch(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            EventManager.OnEnterKeyboardAreaEvent?.Invoke(false);
            UiSwitch(false);
        }
    }

    private void UiSwitch(bool state)
    {
        inventoryUi.SetActive(state);
        tabsUI.SetActive(state);
    }
}