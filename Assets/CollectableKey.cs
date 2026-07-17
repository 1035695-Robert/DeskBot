using System;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
   [SerializeField] private string keyName;
   [SerializeField] private GameObject uiKeyPrefab;
   [SerializeField] private GameObject keyInventory;

   private void Awake()
   { 
      keyInventory = GameObject.Find("key inventory");
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         uiKeyPrefab = Resources.Load<GameObject>(keyName);
         GameObject newKey =Instantiate( uiKeyPrefab, keyInventory.transform.position, Quaternion.identity);
         newKey.name = newKey.name.Replace("(Clone)", "");
         newKey.transform.SetParent(keyInventory.transform, false);
         transform.root.gameObject.SetActive(false);
      }
         
   }
}
