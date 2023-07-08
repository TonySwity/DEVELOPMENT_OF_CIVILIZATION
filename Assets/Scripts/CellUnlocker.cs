using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellUnlocker : MonoBehaviour
{
    private List<ICellable> _cells = new List<ICellable>();

    private void Start()
    {
        _cells = transform.GetComponentsInChildren<ICellable>().ToList();
        _cells.ForEach(a => a.Achieved += UnlockAge);
    }
    
    private void UnlockAge(ItemType itemType)
    {
        print($"{itemType}");
    }

    private void OnDisable()
    {
        _cells.ForEach(a => a.Achieved -= UnlockAge);
    }
}
