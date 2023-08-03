using DG.Tweening;
using UnityEngine;
public class Dragon : Enemy
{
    [SerializeField] private PathType _pathType;
    [SerializeField]private float _duration = 10f;
    
    private Vector3[] _currentPath = {};
    private Vector3 _startPosition;
    
    private void OnEnable()
    {
        if (_currentPath.Length > 0)
        {
            Move();
        }
    }
    
    public void SetPath(Vector3[] path)
    {
        _currentPath = new Vector3[path.Length];
        _currentPath = path;
    }

    protected override void Move()
    { 
        transform.DOPath(_currentPath, _duration, _pathType).SetLookAt(0.01f).OnComplete(() =>
        {
            transform.DOComplete();
            gameObject.SetActive(false);
        });
    }
    
}
