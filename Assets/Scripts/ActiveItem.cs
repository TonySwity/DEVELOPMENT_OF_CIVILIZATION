using UnityEngine;

public class ActiveItem : Item
{
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

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out ActiveItem activeItem))
        {
            if (ItemID < activeItem.ItemID)
            {
                if (IsActivateMerge && activeItem.IsActivateMerge)
                {
                    _mergeSystem?.Collapse(this, activeItem);
                }
            }
        }
    }
}
