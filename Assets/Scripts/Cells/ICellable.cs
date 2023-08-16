using System;
using UnityEngine;

public interface ICellable
{
    public ItemType CurrentItemType { get; }
    
    public event Action<ItemType> Achieved;
    public event Action<Vector3> Busied;
    public void SayState();
    public void SetCurrentItemType(ItemType itemType);
    public void SetColorMaterial(Material material);
    public void EnableCollider();
    public void DisableCollider();
}