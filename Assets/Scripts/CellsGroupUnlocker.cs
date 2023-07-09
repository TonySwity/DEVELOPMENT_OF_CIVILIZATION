using System.Collections.Generic;
using UnityEngine;

public class CellsGroupUnlocker : MonoBehaviour
{
    [SerializeField] private Material _blockMaterial;
    [SerializeField] private Material _unlockMaterial;
    
    private List<ICellable> _cells = new List<ICellable>();
    
    public void Block()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].SetColorMaterial(_blockMaterial);
            _cells[i].DisableCollider();
        }
    }

    public void Unlock()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].SetColorMaterial(_unlockMaterial);
            _cells[i].EnableCollider();
        }
    }
}
