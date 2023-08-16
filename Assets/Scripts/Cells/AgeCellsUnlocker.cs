using UnityEngine;

public class AgeCellsUnlocker : MonoBehaviour
{
    [SerializeField] private Material _blockMaterial;
    [SerializeField] private Material _unlockMaterial;
    
    private ICellable[] _cells = {};
    
    public bool IsBlock { get; private set; }

    public void Initialize()
    {
        _cells = transform.GetComponentsInChildren<ICellable>();
    }

    public void Block()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].SetColorMaterial(_blockMaterial);
            _cells[i].DisableCollider();
        }

        IsBlock = true;
    }

    public void Unlock()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].SetColorMaterial(_unlockMaterial);
            _cells[i].EnableCollider();
        }

        IsBlock = false;
    }
}
