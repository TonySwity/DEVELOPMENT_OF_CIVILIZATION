using System;
using UnityEngine;

[RequireComponent(typeof(ActiveItem))]
public class SelectableObject : MonoBehaviour
{
    public ActiveItem ActiveItem { get; private set; }

    private void Start()
    {
        ActiveItem = GetComponent<ActiveItem>();
    }
    
    public virtual void OnHover()
    {
        transform.localScale = Vector3.one * 1.1f;
        ActiveItem.DeactivateMerge();
    }

    public virtual void OnUnhover()
    {
        transform.localScale = Vector3.one;
        ActiveItem.ActivatedMerge();
    }

    public ItemType GetCurrentItemType() => ActiveItem.CurrentItemType;

}
