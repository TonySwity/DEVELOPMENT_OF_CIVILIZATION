using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemySpawner : EnemyPool
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _timer;
    [SerializeField] private Transform _zone;
    [SerializeField] private EnemyWave[] _waves;

    private EnemyWave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timerAfterLastSpawn;
    private int _create;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_currentWave == null)
        {
            return;
        }

        _timerAfterLastSpawn += Time.deltaTime;

        if (_timerAfterLastSpawn >= _currentWave.Delay)
        {
            
        }
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private Vector3 GetPointInsideZone()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        float z = 0f;

        return _zone.TransformPoint(x, y, z);
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,transform.localScale);
    }
  #endif

}
