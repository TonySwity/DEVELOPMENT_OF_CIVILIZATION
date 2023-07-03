using System;
using UnityEngine;

public class Spawner : ActiveItemPool
{
    [SerializeField] private ActiveItem[] _activeItems = new ActiveItem[]{};
    [SerializeField] private Cell _spawnPoint;

    private int _iDCounter = 0;

    private int _counter = 0;
    
    private Vector3 _offset = Vector3.up * 0.2f;
    
    private void Start()
    {
        for (int i = 0; i < _activeItems.Length - 1; i++)
        {
            ActiveItem activeItem = _activeItems[i];
            
            for (int j = 0; j < CapacityOfEachType; j++)
            {
                Initialize(activeItem);
            }
        }
    }
    
    public void SpawnTree()
    {
        if (TryGetActiveItem(ItemType.Tree, out ActiveItem result))
        {
            SetActiveItem(result, _spawnPoint);
        }
        
        MergeSystem.Instance.Merrged += SpawnNextActiveItem;
    }

    public void SpawnMan()
    {
        if (TryGetActiveItem(ItemType.Man, out ActiveItem result))
        {
            SetActiveItem(result, _spawnPoint);
        }
    }

    private void SpawnNextActiveItem(ActiveItem currentActiveItem)
    {
        if(TryGetActiveItem(currentActiveItem.NextItem, out ActiveItem result))
        {
            currentActiveItem.CurrentCell.SetCurrentItemType(ItemType.Empty);
            SetActiveItem(result, currentActiveItem.CurrentCell);
        }
        currentActiveItem.gameObject.SetActive(false);
        MergeSystem.Instance.Merrged -= SpawnNextActiveItem;
    }
    
    private void SetActiveItem(ActiveItem activeItem, Cell cell)
    {

        Debug.Log(_counter++);
        if (cell.CurrentItemType != ItemType.Empty)
        {
            return;
        }
        
        ChangeIDActiveItem(activeItem);
        print(activeItem.ItemID);
        activeItem.gameObject.SetActive(true);
        activeItem.SetCurrentCell(cell);
        cell.SetCurrentItemType(activeItem.CurrentItemType);
        activeItem.transform.position = cell.transform.position + _offset;
    }

    private void ChangeIDActiveItem(ActiveItem activeItem)
    {
        _iDCounter++;
        activeItem.AddItemID(_iDCounter);
    }
}
