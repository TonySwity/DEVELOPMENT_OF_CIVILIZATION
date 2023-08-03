using UnityEngine;

public class AgeGroupsUnlocker : MonoBehaviour
{
    [SerializeField] private AgeCellsUnlocker[] _ages = {};
    [SerializeField] private Wallet _wallet;
    [SerializeField] private EnemySpawner _enemySpawner;

    private ICellable[] _cells = {};

    private void Start()
    {
        _enemySpawner.DisActivateZombieAttack();
        _enemySpawner.DisActivateDragonAttack();
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
                _enemySpawner.ActivateZombiesAttack();
                break;
            case ItemType.Neo when _ages[(int)AgeItem.Modern].IsBlock:
                _ages[(int)AgeItem.Modern].Unlock();
                _enemySpawner.DisActivateZombieAttack();
                _enemySpawner.ActivateDragonAttack();
                break;
            case ItemType.Grogu when _ages[(int)AgeItem.Future].IsBlock:
                _ages[(int)AgeItem.Future].Unlock();
                _enemySpawner.ActivateZombiesAttack();
                _enemySpawner.ActivateDragonAttack();
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
