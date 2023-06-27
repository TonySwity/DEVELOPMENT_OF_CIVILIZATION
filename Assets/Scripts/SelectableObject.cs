using System;
using UnityEngine;

[RequireComponent(typeof(ActiveItem))]
public class SelectableObject : MonoBehaviour
{
    private ActiveItem _activeItem;

    private void Start()
    {
        _activeItem = GetComponent<ActiveItem>();
    }
    public virtual void OnHover()
    {
        transform.localScale = Vector3.one * 1.1f;
        _activeItem.DeactivateMerge();
    }

    public virtual void OnUnhover()
    {
        transform.localScale = Vector3.one;
        _activeItem.ActivatedMerge();
    }

    public ItemType GetCurrentItemType() => _activeItem.CurrentItemType;

}
