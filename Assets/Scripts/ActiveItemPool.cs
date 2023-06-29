using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveItemPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacityOfEachType = 20;

    private List<ActiveItem> _pool = new List<ActiveItem>();
    
    protected void Initialize(ActiveItem activeItem)
    {
        ActiveItem spawned = Instantiate(activeItem, _container);
        spawned.gameObject.SetActive(false);
        _pool.Add(spawned);
    }

    protected bool TryGetActiveItem(ItemType itemType, out ActiveItem activeItemResult)
    {
        activeItemResult = _pool.FirstOrDefault(a => a.gameObject.activeSelf == false && a.CurrentItemType == itemType);

        return activeItemResult != null;
    }
}
