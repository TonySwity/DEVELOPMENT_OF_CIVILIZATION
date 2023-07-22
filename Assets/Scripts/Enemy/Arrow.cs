using DG.Tweening;
using UnityEngine;
public class Arrow : Enemy
{
    [SerializeField] private PathType _pathType;

    private float _duration = 7f;
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
