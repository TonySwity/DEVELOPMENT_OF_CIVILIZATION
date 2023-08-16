using UnityEngine;

public class ActiveItemSpawner : ActiveItemPool
{
    [SerializeField] private ActiveItem[] _activeItems = new ActiveItem[]{};
    [SerializeField] private Cell _spawnPoint;
    
    private readonly ItemType[] _itemTypes =
    {
        ItemType.Sheep,
        ItemType.EndermanBaby,
        ItemType.Enderman,
        ItemType.Totem,
        ItemType.Skeleton,
        ItemType.Man,
        ItemType.Women,
        ItemType.SpiderMan,
        ItemType.Neo,
        ItemType.Grogu
    };
    private Wallet _wallet;
    private MergeSystem _mergeSystem;
    private int _activeIndexType = 0;
    private int _iDCounter = 0;
    private ItemType _currentActiveItemSpawn;

    [field: SerializeField]public int CapacityOfEachType { get; private set; } = 20;
    
    public void Initialize(Wallet wallet, MergeSystem mergeSystem)
    {
        _wallet = wallet;
        _mergeSystem = mergeSystem;
        
        foreach (var activeItem in _activeItems)
        {
            for (int j = 0; j < CapacityOfEachType; j++)
            {
                Initialize(activeItem);
            }
        }
        
        _wallet.LevelUpped += SetSpawnActiveItem;
        _currentActiveItemSpawn = _itemTypes[_activeIndexType];
        _mergeSystem.Spawned += SpawnNextActiveItem;
    }
    
    public void Spawn()
    {
        if ( _spawnPoint.CurrentItemType == ItemType.Empty && TryGetActiveItemFromPool(_currentActiveItemSpawn, out ActiveItem result) && _wallet.TryBuy())
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
        if (TryGetActiveItemFromPool(activeItem.NextItem, out ActiveItem result) == false)
        {
            return;
        }
        
        result.transform.position = activeItem.transform.position;
        activeItem.ReturnToPool();
        result.GetFromPool();
        ChangeIDActiveItem(result);
        result.Merged += _mergeSystem.Collapse;
        result.OnUnhover();
        _wallet.AddMoneyWhenNextActiveItemSpawn();
    }

    private void SetSpawnActiveItem(int levelIndex)
    {
        _activeIndexType = levelIndex;
        
        if (_activeIndexType < _itemTypes.Length)
        {
            _currentActiveItemSpawn = _itemTypes[_activeIndexType];
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
        _wallet.LevelUpped -= SetSpawnActiveItem;
    }
}
