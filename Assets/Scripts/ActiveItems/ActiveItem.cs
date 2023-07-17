using System;
using UnityEngine;

public class ActiveItem : Item
{
    [SerializeField] private LayerMask _layerMask;

    private MergeSystem _mergeSystem;
    private float _radiusSphere = 0.9f;
    
    public bool IsPaired { get; private set; } = false;
    public Cell CurrentCell { get; private set; }
    public ItemType CurrentItemType => ItemType;
    public ItemType NextItem => NextItemType;
    public int ItemID { get; private set; }
    public bool IsActivateMerge { get; private set; }

    private void OnEnable() => IsPaired = false;
    
    public void Init(MergeSystem mergeSystem) => _mergeSystem = mergeSystem;
    
    public void SetCurrentCell(Cell currentCell) => CurrentCell = currentCell;
    
    public void AddItemID(int itemID) => ItemID = itemID;
    
    public void ActivatedMerge() => IsActivateMerge = true;
    
    public void DeactivateMerge() => IsActivateMerge = false;
    
    public void ResetItemTypeCell() => CurrentCell.SetCurrentItemType(ItemType.Empty);
    
    public void FindFirstColliderToMerge()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusSphere, _layerMask);
        
        int minColliders = 2;

        if (colliders.Length < minColliders)
        {
            return;
        }
        
        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out ActiveItem activeItem) &&
                CurrentItemType == activeItem.CurrentItemType &&
                ItemID != activeItem.ItemID && IsPaired == false &&
                IsActivateMerge && activeItem.IsActivateMerge)
            {
                _mergeSystem?.Collapse(this, activeItem);
                IsPaired = true;
            }
        }
    }

    private void OnDisable()
    {
        if (CurrentCell)
        {
            CurrentCell.SetCurrentItemType(ItemType.Empty);
        }
    }
}