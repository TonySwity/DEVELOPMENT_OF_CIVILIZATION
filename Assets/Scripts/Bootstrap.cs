using System;
using UnityEngine;
public class Bootstrap: MonoBehaviour
{
    [SerializeField] private ActiveItemSpawner _activeItemSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private AgeGroupsUnlocker _ageGroups;
    [SerializeField] private Wallet _wallet;

    private void Start()
    {
        _ageGroups.Initialize();
        _enemySpawner.Initialize();
        _activeItemSpawner.Initialize();
        _wallet.Initialize();
    }
}

