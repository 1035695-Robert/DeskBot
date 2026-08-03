using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropZoneCheck : MonoBehaviour
{
    public delegate void OnBoxPlacement();

    public OnBoxPlacement onBoxPlacementEvent;
    public OnBoxPlacement onBoxReplacementEvent;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject[] dropBox;
    [SerializeField] int _count = 0;

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

    private void UpdateBoxCount()
    {
        _count++;
        Debug.Log("Box detected");
        if (_count == dropBox.Length)
        {
            KeyGenerator.Instance.CompleteTask(spawnPoint);
        }
    }

    private void RemoveBox()
    {
        _count--;
    }
}