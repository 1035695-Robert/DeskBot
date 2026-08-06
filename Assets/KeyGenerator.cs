using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class KeyGenerator : MonoBehaviour
{
    private Dictionary<string, GameObject> keyDictionary = new Dictionary<string, GameObject>();
    public GameObject inventory;
    public List<string> keyList;
    public GameObject keyPrefab;
    [SerializeField] private int startAmount;

    public static KeyGenerator Instance;

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

    private void Start()
    {
        CreateKeyDictionary();
    }

    private void CreateKeyDictionary()
    {
        GameObject[] keysObjects = Resources.LoadAll<GameObject>("Keys");

        keyDictionary = keysObjects.ToDictionary(
            key => key.name,
            key => key
        );

        keyList = new List<string>(keyDictionary.Keys);

        GivRandomKeys();
    }

    private void GivRandomKeys()
    {
        for (int i = 0; i < startAmount; i++)
        {
            string randomValue = RandomValue();
            if (keyDictionary.TryGetValue(randomValue, out GameObject key))
            {
                GiveNewKey(key, randomValue);
            }
        }
    }

    private string RandomValue()
    {
        if (keyDictionary.Count == 0) return null;

        int randomIndex = Random.Range(0, keyDictionary.Count);
        string randomKey = keyList[randomIndex];
        return keyDictionary[randomKey].name;
    }

    public void InsertKey(GameObject key)
    {
        if (keyDictionary.TryGetValue(key.name, out GameObject keyUi))
        {
            GiveNewKey(keyUi, key.name);
        }
    }

    private void GiveNewKey(GameObject key, string randomValue)
    {
        GameObject newKey = Instantiate(key, inventory.transform);
        newKey.name = key.name.Replace("(Clone)", "");
        keyDictionary.Remove(randomValue);
        EventManager.OnInsertKeyEvent?.Invoke();
        Debug.Log(randomValue);
        keyList.Remove(randomValue);
    }

    public Task CompleteTask(Transform keySpawnPointFromTask)
    {
        try
        {
            string randomValue = RandomValue();
            Debug.Log(randomValue);
            GameObject newPhysicalKey = Instantiate(keyPrefab, keySpawnPointFromTask.transform.position,Quaternion.identity);
            newPhysicalKey.name = randomValue;
            TextMeshPro displayName = newPhysicalKey.GetComponentInChildren<TextMeshPro>();
            if (displayName == null) Debug.LogError("missing");
            displayName.text = newPhysicalKey.name;
            return Task.CompletedTask;
        }
        catch (Exception exception)
        {
            return Task.FromException(exception);
        }
    }
}