using System;
using System.Linq;
using UnityEngine;

public class ActiveItem : Item
{
    [SerializeField] private LayerMask _layerMask;

    private MergeSystem _mergeSystem;
    [field: SerializeField] public bool IsPared { get; private set; } = false;
    private float _radiusSphere = 0.9f;
    
    public Cell CurrentCell { get; private set; }
    public ItemType CurrentItemType => ItemType;
    public ItemType NextItem => NextItemType;
    [field: SerializeField] public int ItemID { get; private set; }
    [field: SerializeField] public bool IsActivateMerge { get; private set; }

    public void Init(MergeSystem mergeSystem)
    {
        _mergeSystem = mergeSystem;
    }

    public void SetCurrentCell(Cell currentCell) => CurrentCell = currentCell;
    public void AddItemID(int itemID) => ItemID = itemID;
    public void ActivatedMerge() => IsActivateMerge = true;
    public void DeactivateMerge() => IsActivateMerge = false;
    public void ResetItemTypeCell() => CurrentCell.SetCurrentItemType(ItemType.Empty);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, _radiusSphere);
    }

    public void FindFirstColliderToMerge()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusSphere, _layerMask);

        int minColliders = 2;
        int secondCollider = 0;

        if (colliders.Length >= minColliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out ActiveItem activeItem))
                {
                    if (ItemID != activeItem.ItemID && IsPared == false)
                    {
                        if (IsActivateMerge && activeItem.IsActivateMerge)
                        {
                            _mergeSystem?.Collapse(this, activeItem);
                            IsPared = true;
                        }
                    }
                }
            }
        }

        IsPared = false;
    }
}
