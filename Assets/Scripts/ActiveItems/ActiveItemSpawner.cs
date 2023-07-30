using UnityEngine;

public class ActiveItemSpawner : ActiveItemPool
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ActiveItem[] _activeItems = new ActiveItem[]{};
    [SerializeField] private Cell _spawnPoint;
    [SerializeField] private MergeSystem _mergeSystem;

    private int _iDCounter = 0;
    private int _activeItemPrice = 17;
    private int _incomePrice = 40;
    private int _increaseIncome = 2;
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
    
    public void SpawnTree()
    {
        if (_wallet.TryBuy(_activeItemPrice) && TryGetActiveItem(ItemType.Tree, out ActiveItem result))
        {
            result.Merged += _mergeSystem.Collapse;
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
    
    public void IncreaseIncome()
    {
        if (_wallet.TryBuy(_incomePrice))
        {
            _wallet.AddIncome(_increaseIncome);
            _wallet.IncreasePriseIncome(_incomePrice);
        }
    }

    public void SpawnNextActiveItem(ActiveItem activeItem)
    {
        if(TryGetActiveItem(activeItem.NextItem, out ActiveItem result))
        {
            Debug.Log("next");
            result.transform.position = activeItem.transform.position;
            activeItem.gameObject.SetActive(false);
            result.gameObject.SetActive(true);
            ChangeIDActiveItem(result);
            result.Merged += _mergeSystem.Collapse;
            result.OnUnhover();
        }
    }
    
    private void SetActiveItem(ActiveItem activeItem, Cell cell)
    {
        if (cell.CurrentItemType != ItemType.Empty)
        {
            return;
        }
        
        ChangeIDActiveItem(activeItem);
        activeItem.gameObject.SetActive(true);
        activeItem.transform.position = new Vector3(cell.transform.position.x, Constants.DragObject.OffsetY, cell.transform.position.z);
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
