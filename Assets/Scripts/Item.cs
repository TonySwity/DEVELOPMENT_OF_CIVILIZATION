using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Empty,
    Tree,
    Stump,
    Tent
    
}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    
    public ItemType ItemType => _itemType;
}
