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
    public ItemType ItemType;
}
