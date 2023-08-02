using UnityEngine;

public class AgeGroupsUnlocker : MonoBehaviour
{
    [SerializeField] private AgeCellsUnlocker[] _ages = {};
    [SerializeField] private Wallet _wallet;

    private ICellable[] _cells = {};

    private void Start()
    {
        _ages = transform.GetComponentsInChildren<AgeCellsUnlocker>();
        _cells = transform.GetComponentsInChildren<ICellable>();

        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].Achieved += CheckAchievement;
            _cells[i].Achieved += _wallet.SetCountCells;
        }

        for (int i = 0; i < _ages.Length; i++)
        {
            if (i >= (int)AgeItem.Iron)
            {
                _ages[i].Block();
            }
        }
    }

    private void CheckAchievement(ItemType itemType)
    {
        if (itemType == ItemType.Man && _ages[(int)AgeItem.Iron].IsBlock)
        {
            _ages[(int)AgeItem.Iron].Unlock();
        }
        
        if (itemType == ItemType.SpiderMan && _ages[(int)AgeItem.Classic].IsBlock)
        {
            _ages[(int)AgeItem.Classic].Unlock();
        }
        
        if (itemType == ItemType.Neo && _ages[(int)AgeItem.Modern].IsBlock)
        {
            _ages[(int)AgeItem.Classic].Unlock();
        }
        
        if (itemType == ItemType.Grogu && _ages[(int)AgeItem.Future].IsBlock)
        {
            _ages[(int)AgeItem.Classic].Unlock();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].Achieved -= CheckAchievement;
            _cells[i].Achieved -= _wallet.SetCountCells;
        }
    }
}
