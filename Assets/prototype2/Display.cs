
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Display : MonoBehaviour
{
    public LayerMask displayUI;
    public GameObject keyboardDisplay;
    public InputAction displayToggle;
    
    public void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
           keyboardDisplay.SetActive(!keyboardDisplay.activeSelf);
        }
    }
}
