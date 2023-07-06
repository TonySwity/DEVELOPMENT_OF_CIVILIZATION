using UnityEngine;

public class Spawner : ActiveItemPool
{
    [SerializeField] private ActiveItem[] _activeItems = new ActiveItem[]{};
    [SerializeField] private Cell _spawnPoint;
    [SerializeField] private MergeSystem _mergeSystem;

    private int _iDCounter = 0;
    private Vector3 _offset = Vector3.up * 0.2f;
    
    private void Start()
    {
        _mergeSystem.Init(this);
        
        for (int i = 0; i < _activeItems.Length; i++)
        {
            ActiveItem activeItem = _activeItems[i];
            
            for (int j = 0; j < CapacityOfEachType; j++)
            {
                Initialize(activeItem, _mergeSystem);
            }
        }
    }
    
    public void SpawnTree()
    {
        if (TryGetActiveItem(ItemType.Tree, out ActiveItem result))
        {
            SetActiveItem(result, _spawnPoint);
        }
    }

    public void SpawnMan()
    {
        if (TryGetActiveItem(ItemType.Man, out ActiveItem result))
        {
            SetActiveItem(result, _spawnPoint);
        }
    }

    public void SpawnNextActiveItem(ActiveItem currentActiveItem)
    {
        if(TryGetActiveItem(currentActiveItem.NextItem, out ActiveItem result))
        {
            currentActiveItem.CurrentCell.SetCurrentItemType(ItemType.Empty);
            SetActiveItem(result, currentActiveItem.CurrentCell);
            result.ActivatedMerge();
            result.FindFirstColliderToMerge();
        }
        
        currentActiveItem.gameObject.SetActive(false);
    }
    
    private void SetActiveItem(ActiveItem activeItem, Cell cell)
    {
        if (cell.CurrentItemType != ItemType.Empty)
        {
            return;
        }
        
        ChangeIDActiveItem(activeItem);
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
