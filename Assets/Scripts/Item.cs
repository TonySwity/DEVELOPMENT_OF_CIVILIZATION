using UnityEngine;

public enum ItemType
{
    Empty,
    Man,
    Tree,
    Stump,
    Tent
}

public class Item : MonoBehaviour
{
    [SerializeField] protected ItemType ItemType;
    
    public ItemType CurrentItemType => ItemType;
}
