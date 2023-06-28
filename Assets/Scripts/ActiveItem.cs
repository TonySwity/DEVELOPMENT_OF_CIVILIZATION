using UnityEngine;

public class ActiveItem : Item
{
    private Cell _currentCell;
    //
    private MergeSystem _mergeSystem;
    //
    public ItemType CurrentItemType  => ItemType; 
    
    [field: SerializeField] private ItemType NextItem;
    [field: SerializeField] public int ItemID { get; private set; }

    [field: SerializeField] public bool IsActivateMerge { get; private set; }

    //first
    public void Init(MergeSystem mergeSystem) => _mergeSystem = mergeSystem;
    //

    public void SetCurrentCell(Cell currentCell)
    {
        _currentCell = currentCell;
    }
    
    public void AddItemID(int itemID) => ItemID = itemID;

    public void ActivatedMerge() => IsActivateMerge = true;

    public void DeactivateMerge() => IsActivateMerge = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out ActiveItem activeItem) == false)
        {
            return;
        }
        
        if (IsActivateMerge && activeItem.IsActivateMerge)
        {
            MergeSystem.Instance.Collapse(this, activeItem);
            
            //
            //_mergeSystem.Collapse(this, activeItem);
            //
        }
    }
    
    public void ResetItemTypeCell() => _currentCell.SetCurrentItemType(ItemType.Empty);
}
