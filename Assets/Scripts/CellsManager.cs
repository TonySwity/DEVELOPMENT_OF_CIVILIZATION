using System.Collections.Generic;
using UnityEngine;

public class CellsManager : MonoBehaviour
{
    [field: SerializeField] private List<Cell> _availableCells = new List<Cell>();

    private void Start()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
    }
}
