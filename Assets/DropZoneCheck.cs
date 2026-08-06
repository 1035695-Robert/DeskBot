using System;
using UnityEngine;

public class DropZoneCheck : MonoBehaviour
{
    public delegate void OnBoxPlacement();

    public OnBoxPlacement onBoxPlacementEvent;
    public OnBoxPlacement onBoxReplacementEvent;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject[] dropBox;
    [SerializeField] int _count = 0;
    private bool isCompleted = false;

    private void OnEnable()
    {
        onBoxPlacementEvent += UpdateBoxCount;
        onBoxReplacementEvent += RemoveBox;
    }

    private void OnDisable()
    {
        onBoxPlacementEvent -= UpdateBoxCount;
        onBoxReplacementEvent -= RemoveBox;
    }

    private async void UpdateBoxCount()
    {
        try
        {
            if (isCompleted) return;
            _count++;
            Debug.Log("Box detected");

            if (_count != dropBox.Length) return;
            isCompleted = true;
            EventManager.OnAudioRequestEvent?.Invoke("TaskCompleted");
            await KeyGenerator.Instance.CompleteTask(spawnPoint);
            gameObject.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void RemoveBox()
    {
        _count--;
    }
}