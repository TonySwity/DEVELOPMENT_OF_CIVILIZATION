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
        switch (itemType)
        {
            case ItemType.Man when _ages[(int)AgeItem.Iron].IsBlock:
                _ages[(int)AgeItem.Iron].Unlock();
                break;
            case ItemType.SpiderMan when _ages[(int)AgeItem.Classic].IsBlock:
                _ages[(int)AgeItem.Classic].Unlock();
                break;
            case ItemType.Neo when _ages[(int)AgeItem.Modern].IsBlock:
                _ages[(int)AgeItem.Modern].Unlock();
                break;
            case ItemType.Grogu when _ages[(int)AgeItem.Future].IsBlock:
                _ages[(int)AgeItem.Future].Unlock();
                break;
            default:
                print("Произошла незапланированная фигня");
                break;
                
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
