using System;
using UnityEngine;

public class Cell : MonoBehaviour, ICellable
{
    [field: SerializeField] public ItemType CurrentItemType { get; private set; }
    [field: SerializeField] public Color CurrentColor { get; private set; }
    
    public event Action<ItemType> Achieved;
    
    
    public void SetCurrentItemType(ItemType itemType)
    {
        CurrentItemType = itemType;
        Achieved?.Invoke(CurrentItemType);
    }
    public void SetColor(Color color)
    {
        CurrentColor = color;
    }
}

