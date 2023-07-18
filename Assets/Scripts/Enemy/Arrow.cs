using DG.Tweening;
using UnityEngine;
public class Arrow : Enemy
{
    [SerializeField] private EnemyPath _enemyPath;
    [SerializeField] private PathType _pathType;

    private float _duration = 7f;
    
    private void Start()
    {
        Move();
    }

    protected override void Move()
    {
        transform.DOPath(_enemyPath.GetNewPath(), _duration, _pathType).SetLookAt(0.01f);
    }
}
