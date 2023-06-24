using UnityEngine;

public class ActiveItem : Item
{
    [field: SerializeField] private ItemType NextItem;
    [field: SerializeField]public int ItemID { get; private set; }

    public void AddItemID(int itemID)
    {
        ItemID = itemID;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out ActiveItem activeItem))
        {
            MergeSystem.Instance.Collapse(this, activeItem);
        }
    }
}
