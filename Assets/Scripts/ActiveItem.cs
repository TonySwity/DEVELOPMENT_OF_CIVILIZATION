using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : Item
{
    [SerializeField] private LayerMask _layerMask;
    private Cell _currentCell;
    private MergeSystem _mergeSystem;
    
    public Cell CurrentCell => _currentCell;
    public ItemType CurrentItemType => ItemType;
    public ItemType NextItem => NextItemType;
    [field: SerializeField] public int ItemID { get; private set; }
    [field: SerializeField] public bool IsActivateMerge { get; private set; }

    public void Init(MergeSystem mergeSystem)
    {
        _mergeSystem = mergeSystem;
    }

    public void SetCurrentCell(Cell currentCell) => _currentCell = currentCell;

    public void AddItemID(int itemID) => ItemID = itemID;

    public void ActivatedMerge() => IsActivateMerge = true;

    public void DeactivateMerge() => IsActivateMerge = false;

    public void ResetItemTypeCell() => _currentCell.SetCurrentItemType(ItemType.Empty);
    
    public void FindFirstColliderToMerge()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.7f, _layerMask);
        
        int minColliders = 2;
        int secondCollider = 0;

        if (colliders.Length >= minColliders)
        {
            for (int i = 0; i <colliders.Length-1; i++)
            {
                if (colliders[i].TryGetComponent(out ActiveItem activeItem))
                {
                    if (ItemID != activeItem.ItemID && activeItem.ItemID < ItemID)
                    {
                        if (IsActivateMerge && activeItem.IsActivateMerge)
                        {
                            _mergeSystem?.Collapse(this, activeItem);
                        }
                    }
                }
            }

        }
    }
}
