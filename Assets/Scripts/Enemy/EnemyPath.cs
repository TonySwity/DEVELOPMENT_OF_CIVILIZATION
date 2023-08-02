using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyPath : MonoBehaviour
{
    [SerializeField] private float _minX = -5f;
    [SerializeField] private float _maxX = 5f;
    
    private Vector3[] _path = { };
    private Transform[] _tempPath;
    
    private void Awake()
    {
         _tempPath = GetComponentsInChildren<Transform>();
        _path = new Vector3[_tempPath.Length];
    }

    public Vector3[] GetNewPath()
    {
        for (int i = 0; i < _tempPath.Length; i++)
        {
            float tempX = Random.Range(_minX, _maxX);
            _tempPath[i].position = new Vector3(tempX, _tempPath[i].position.y, _tempPath[i].position.z);
            _path[i] = _tempPath[i].position;
        }

        return _path;
    }
}
