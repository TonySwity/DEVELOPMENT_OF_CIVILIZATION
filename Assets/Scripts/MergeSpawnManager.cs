using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSpawnManager : MonoBehaviour
{
    [field: SerializeField] private List<Cell> _availableCells = new List<Cell>();

    private void Start()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        
    }
}
