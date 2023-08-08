using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : EnemyPool
{
    [SerializeField] private Transform _zone;
    [SerializeField] private EnemyPath _enemyPath;
    [SerializeField] private EnemyWave[] _waves;

    private EnemyWave _currentWave;
    private float _timerAfterLastSpawn = 0f;
    private bool _isZombiesAttack;
    private bool _isDragonsAttack;
    private int _zombiesIndex = 0;
    private int _dragonIndex = 1;
    
    public void Initialize()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            for (int j = 0; j < _waves[i].Count; j++)
            {
                Initialize(_waves[i].Tamplate);
            }
        }

        _currentWave = null;
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
            AttackZombies();
            AttackDragons();
         }
    }
    
    public void ActivateZombiesAttack()
    {
        _isZombiesAttack = true;
        _currentWave = _waves[_zombiesIndex];
    }

    public void DisActivateZombieAttack()
    {
        _isZombiesAttack = false;
        _currentWave = null;
    }

    public void ActivateDragonAttack()
    {
        _isDragonsAttack = true;
        _currentWave = _waves[_dragonIndex];
    }
    
    public void DisActivateDragonAttack()
    {
        _isDragonsAttack = false;
        _currentWave = null;
    }
    
    private void AttackZombies()
    {
        if (_isZombiesAttack && TryGetEnemyObject(AgeItem.Modern, out Enemy resultEnemy))
        {
            resultEnemy.transform.position = GetPointInsideZone();
            resultEnemy.GetFromPool();
            _timerAfterLastSpawn = 0f;
        }
    }
    
    private void AttackDragons()
    {
        if(_isDragonsAttack && TryGetEnemyObject(AgeItem.Future, out Dragon resultEnemyArrow))
        {
            resultEnemyArrow.transform.position = GetPointInsideZone();
            resultEnemyArrow.SetPath(_enemyPath.GetNewPath());
            resultEnemyArrow.GetFromPool();
            _timerAfterLastSpawn = 0f;
        }
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
