using System;
using UnityEngine;
using Random = System.Random;

public class DropZoneCheck : MonoBehaviour
{
    public delegate void OnBoxPlacement();

    public OnBoxPlacement onBoxPlacementEvent;
    public OnBoxPlacement onBoxReplacementEvent;
    [SerializeField] private Transform keySpawnPoint;
    [SerializeField] private float spawnLength;
    [SerializeField] private GameObject[] dropBox;
    [SerializeField] private Transform spawnPoints;
    [SerializeField] int _count = 0;
    [SerializeField] private bool isCompleted = false;
    private float spacing;

    private void OnEnable()
    {
        onBoxPlacementEvent += UpdateBoxCount;
        onBoxReplacementEvent += RemoveBox;
        EventManager.OnInsertKeyEvent += ResetPuzzle;
        ResetPuzzle();
    }

    void Start()
    {
        spacing = spawnLength / (dropBox.Length - 1);
        ResetPuzzle();
    }

    private void ResetPuzzle()
    {
        _count = 0;
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }

        for (int i = 0; i < dropBox.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);

            (dropBox[i], dropBox[randomIndex]) = (dropBox[randomIndex], dropBox[i]);
        }

        for(int i = 0 ; i < dropBox.Length; i++)
        {
            Vector3 spawnPosition = spawnPoints.position + new Vector3( i * spacing, 0, 0);
            dropBox[i].transform.position = spawnPosition;
        }
        isCompleted = false;
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
            await KeyGenerator.Instance.CompleteTask(keySpawnPoint);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void RemoveBox()
    {
        _count--;
    }
}