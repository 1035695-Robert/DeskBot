using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BindingList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyReferenceText;
    [SerializeField] List<string> keyReferenceList = new List<string>();

    private Dictionary<AbilityList, string> keyDictionary = new Dictionary<AbilityList, string>();

    //private AbilityList abilities;
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
        keyReferenceList.Clear();
        foreach (AbilityList ability in Enum.GetValues(typeof(AbilityList)))
        {
            keyDictionary.Add(ability, null);
        }
    }

    public void AddToList(string keyName, AbilityList ability)
    {
        if (keyDictionary.ContainsKey(ability))
        {
            keyDictionary[ability] = keyName;
            UpdateText();
        }
    }
    // DICTIONARY =<TKeys, TValues>
    //  keyDictionary Keys: Ability, Value: keyName.

    public void RemoveFromList(AbilityList ability)
    {
        keyDictionary[ability] = null;
        UpdateText();
    }

    private void UpdateText()
    {
        keyReferenceText.text = string.Join("\n", keyDictionary
            .Where(key => key.Value != null)
            .Select(key => $"{key.Key}<pos=150> : <pos=200>{key.Value}"));
    }
}