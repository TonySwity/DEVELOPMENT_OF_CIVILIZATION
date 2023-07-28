using System;
using UnityEngine;

public class ActiveItem : SelectableObject
{
    [SerializeField] private LayerMask _layerMaskActiveItem;
    [field: SerializeField]public ItemType CurrentItemType { get; private set; }
    [field: SerializeField]public ItemType NextItem { get; private set; }
    
    public bool IsPaired { get; private set; } = false;
    public int ItemID { get; private set; }
    public bool IsActivateMerge { get; private set; }

    public event Action<ActiveItem, ActiveItem> Merged;

    private void OnEnable()
    {
        IsPaired = false;
    }

    public void AddItemID(int itemID) => ItemID = itemID;
    
    public override void OnHover()
    {
        base.OnHover();
        DeactivateMerge();
    }

    public override void OnUnhover()
    {
        base.OnUnhover();
        ActivatedMerge();
        FindActiveItemToMerge();
    }
    
    public void FindActiveItemToMerge()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Constants.ActiveItemMerge.RadiusSphere, _layerMaskActiveItem);
        int minColliders = 2;
        
        Debug.Log("count colliders = " + colliders.Length);
        
        
        if (colliders.Length < minColliders)
        {
            return;
        }

        foreach (var col in colliders)
        {
            Debug.Log("ID col = " + col.GetComponent<ActiveItem>().ItemID);
        }
        
        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out ActiveItem activeItem) &&
                CurrentItemType == activeItem.CurrentItemType &&
                ItemID != activeItem.ItemID && IsPaired == false &&
                IsActivateMerge && activeItem.IsActivateMerge)
            {
                Debug.Log("mer");
                IsPaired = true;
                Merged?.Invoke(this, activeItem);
            }
        }
    }
    
    private void ActivatedMerge() => IsActivateMerge = true;
    private void DeactivateMerge() => IsActivateMerge = false;
}
