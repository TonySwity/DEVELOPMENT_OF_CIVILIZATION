using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private MergeSystem _mergeSystem;
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private ActiveItemSpawner _activeItemSpawner;
    [SerializeField] private CoinAdder _coinAdder;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private AgeGroupsUnlocker _ageGroupsUnlocker;
    [SerializeField] private int _walletValue = 40;
    [SerializeField] private PausePanel _pausePanel;
    private void Start()
    {
        _wallet.Initialize(_walletValue);
        _mergeSystem.Initialize();
        _activeItemSpawner.Initialize(_wallet, _mergeSystem, _gameCamera, _pausePanel);
        _coinAdder.Initialize(_wallet);
        _enemySpawner.Initialize();
        _ageGroupsUnlocker.Initialize(_wallet, _enemySpawner, _coinAdder);
    }
}
