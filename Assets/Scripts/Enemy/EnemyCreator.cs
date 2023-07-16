using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyCreator : ObjectPool
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _timer;
    [SerializeField] private Transform _zone;
    [SerializeField] private EnemyWave _wave;
    
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
