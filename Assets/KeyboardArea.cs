using System;
using UnityEngine;

public class KeyboardArea : MonoBehaviour
{
    [SerializeField] private GameObject[] screenUi;
    [SerializeField] BoxCollider hitCollider;

    public bool isActiveState;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            EventManager.OnEnterKeyboardAreaEvent?.Invoke(true);
            UiSwitch(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            EventManager.OnEnterKeyboardAreaEvent?.Invoke(false);
            UiSwitch(false);
        }
    }

    private void UiSwitch(bool state)
    {
        if (!state)
        {
            foreach (var ui in screenUi)
            {
                ui.SetActive(false);
            }
        }
        else
        {
            foreach (var ui in screenUi)
            {
                if (ui.name != "=== Unlockables ===")
                {
                    ui.SetActive(true);
                }
            }
        }
    }
}