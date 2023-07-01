using UnityEngine;

public class Spawner : ActiveItemPool
{
    [SerializeField] private ActiveItem[] _activeItems = new ActiveItem[]{};
    [SerializeField] private Cell _spawnPoint;

    private int _iDCounter = 0;
    private void Start()
    {
        for (int i = 0; i < _activeItems.Length - 1; i++)
        {
            ActiveItem activeItem = _activeItems[i];
            for (int j = 0; j < 20; j++)
            {
                Initialize(activeItem);
            }
        }
    }

    public void Spawn()
    {
        
        print("button");
        if (TryGetActiveItem(ItemType.Tree, out ActiveItem result))
        {
            SetActiveItem(result, _spawnPoint.transform.position);
        }
    }

    private void SetActiveItem(ActiveItem activeItem, Vector3 spawn)
    {

        if (_spawnPoint.CurrentItemType != ItemType.Empty)
        {
            return;
        }
        
        ChangeIDActiveItem(activeItem);
        print(activeItem.ItemID);
        activeItem.gameObject.SetActive(true);
        activeItem.SetCurrentCell(_spawnPoint);
        _spawnPoint.SetCurrentItemType(activeItem.CurrentItemType);
        activeItem.transform.position = _spawnPoint.transform.position;
    }

    private void ChangeIDActiveItem(ActiveItem activeItem)
    {
        _iDCounter++;
        activeItem.AddItemID(_iDCounter);
    }
}
