using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BindingList : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI keyReferenceText;
    [SerializeField] List<string> keyRefrenceList = new List<string>();

    public static BindingList Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        keyRefrenceList.Clear();
    }

    public void AddToList(string newKeyAbility)
    {
        keyRefrenceList.Add(newKeyAbility);
        keyReferenceText.text = string.Join("\n", keyRefrenceList);
    }

    public void RemoveFromList(string searchAbility)
    {
       string keyMatch = keyRefrenceList.Find(key => key.Contains(searchAbility));
        keyRefrenceList.Remove(keyMatch);
        keyReferenceText.text = string.Join("\n", keyRefrenceList);
    }
}