using System;

public interface ICellable
{
    public ItemType CurrentItemType { get; }

    public event Action<ItemType> Achieved; 

    public void SetCurrentItemType(ItemType itemType);
}