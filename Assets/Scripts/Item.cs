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
    [SerializeField] protected ItemType ItemType;
    
    public ItemType CurrentItemType => ItemType;
}
