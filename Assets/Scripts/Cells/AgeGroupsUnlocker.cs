using UnityEngine;

public class AgeGroupsUnlocker : MonoBehaviour
{
    [SerializeField] private CellAnimationMover _cellAnimationMover;
    [SerializeField] private AgeCellsUnlocker[] _ages = {};
    [SerializeField] private AudioSource _cellRegistrationSound;

    private Wallet _wallet;
    private EnemySpawner _enemySpawner;
    private CoinAdder _coinAdder;
    private ICellable[] _cells = {};

    public void Initialize(Wallet wallet, EnemySpawner enemySpawner, CoinAdder coinAdder)
    {
        _wallet = wallet;
        _enemySpawner = enemySpawner;
        _coinAdder = coinAdder;
        _enemySpawner.DisActivateZombieAttack();
        _enemySpawner.DisActivateDragonAttack();
        _cellAnimationMover.Initialize();
        _ages = transform.GetComponentsInChildren<AgeCellsUnlocker>();
        _cells = transform.GetComponentsInChildren<ICellable>();

        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].Busied += _coinAdder.StartCoinToWallet;
            _cells[i].Achieved += CheckAchievement;
            _wallet.AddedMoney += _cells[i].SayState;
        }

        for (int i = 0; i < _ages.Length; i++)
        {
            if (i >= (int)AgeItem.Iron)
            {
                _ages[i].Initialize();
                _ages[i].Block();
            }
        }
    }

    private void CheckAchievement(ItemType itemType)
    {
        _cellRegistrationSound.pitch = Random.Range(Constants.AudioSource.MinPitch, Constants.AudioSource.MaxPitch);
        _cellRegistrationSound.Play();
        
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
            _cells[i].Busied -= _coinAdder.StartCoinToWallet;
            _cells[i].Achieved -= CheckAchievement;
            _wallet.AddedMoney -= _cells[i].SayState;
        }
    }
}
