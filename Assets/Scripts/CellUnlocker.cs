using System.Collections.Generic;
using UnityEngine;

public class CellUnlocker : MonoBehaviour
{
    [field: SerializeField] private List<Cell> _availableCells = new List<Cell>();

    private void Start()
    {
        Cell[] cells = transform.GetComponentsInChildren<Cell>();
        _availableCells.AddRange(cells);
    }
}
