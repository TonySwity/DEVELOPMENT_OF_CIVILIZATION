using UnityEngine;
using DG.Tweening;

public class Stone : Enemy
{
    [SerializeField] private Vector3 _endPosition = new Vector3(0f, 0f, -5f);
    [SerializeField] private float _speed = 7f;
    
    private void OnEnable()
    {
        Move();
    }
    
    protected override void Move()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DORotate(new Vector3(360, 0, 0), 2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1));
        sequence.Join(transform.DOMove(_endPosition, _speed).OnComplete(() =>
        {
            sequence.Kill(true);
            gameObject.SetActive(false);
        }));

    }
    
    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.TryGetComponent(out ActiveItem activeItem))
        {
            print(activeItem);
            activeItem.gameObject.SetActive(false);
        }
    }
}
