using UnityEngine;
using UnityEngine.EventSystems;

public class keyboardController : MonoBehaviour
{
    public GameObject KeyboardPanel;
    public GameObject keySlotPrefab;
    public int slotCount;
   
    
    public GameObject[] keyPrefab;

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
     
    }
}
