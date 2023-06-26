using UnityEngine;

public class Cell : MonoBehaviour
{
    [field: SerializeField] public ItemType CurrentItemType { get; private set; }

    public void SetCurrentItemType(ItemType itemType)
    {
        CurrentItemType = itemType;
    }
}

