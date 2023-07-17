using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : EnemyPool
{
    [SerializeField] private Transform _zone;
    [SerializeField] private EnemyWave[] _waves;

    private EnemyWave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timerAfterLastSpawn = 0f;
    private int _create;

    private void Start()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            for (int j = 0; j < _waves[i].Count; j++)
            {
                Initialize(_waves[i].Tamplate);
            }
        }

        _currentWave = _waves[_currentWaveNumber];
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
            if (TryGetEnemyObject(AgeItem.Iron, out Enemy resultGameObject))
            {
                resultGameObject.gameObject.SetActive(true);
                resultGameObject.transform.position = GetPointInsideZone();
                _timerAfterLastSpawn = 0f;
            }
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
        Gizmos.DrawWireCube(_zone.transform.position,_zone.transform.localScale);
    }
  #endif

}
