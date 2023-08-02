using UnityEngine;

public class ActiveItemSpawner : ActiveItemPool
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ActiveItem[] _activeItems = new ActiveItem[]{};
    [SerializeField] private Cell _spawnPoint;
    [SerializeField] private MergeSystem _mergeSystem;

    private int _iDCounter = 0;
    private ItemType _tempItemType;

    [field: SerializeField]public int CapacityOfEachType { get; private set; } = 20;
    
    private void Start()
    {
        for (int i = 0; i < _activeItems.Length; i++)
        {
            ActiveItem activeItem = _activeItems[i];
            
            for (int j = 0; j < CapacityOfEachType; j++)
            {
                Initialize(activeItem);
            }
        }

        _mergeSystem.Spawned += SpawnNextActiveItem;
    }
    
    public void Spawn()
    {
        if ( _spawnPoint.CurrentItemType == ItemType.Empty && TryGetActiveItemFromPool(ItemType.Sheep, out ActiveItem result) && _wallet.TryBuy())
        {
            ChangeIDActiveItem(result);
            PutActiveItemToSpawnPoint(result, _spawnPoint);
            result.gameObject.SetActive(true);
            result.Merged += _mergeSystem.Collapse;
        }
    }
    
    public void IncreaseIncome()
    {
        _wallet.IncreasePriseIncome();
    }

    public void SpawnNextActiveItem(ActiveItem activeItem)
    {
        if(TryGetActiveItemFromPool(activeItem.NextItem, out ActiveItem result))
        {
            result.transform.position = activeItem.transform.position;
            activeItem.ReturnToPool();
            result.GetFromPool();
            ChangeIDActiveItem(result);
            result.Merged += _mergeSystem.Collapse;
            result.OnUnhover();
        }
    }
    
    private void PutActiveItemToSpawnPoint(ActiveItem activeItem, Cell spawnPointCell)
    {
        if (spawnPointCell.CurrentItemType != ItemType.Empty)
        {
            return;
        }
        
        activeItem.transform.position = new Vector3(spawnPointCell.transform.position.x, Constants.DragObject.OffsetY, spawnPointCell.transform.position.z);
    }
    
    private void ChangeIDActiveItem(ActiveItem activeItem)
    {
        _iDCounter += 1;
        activeItem.AddItemID(_iDCounter);
    }

    private void OnDisable()
    {
        _mergeSystem.Spawned -= SpawnNextActiveItem;
    }
}
