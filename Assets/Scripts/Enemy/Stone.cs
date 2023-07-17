using UnityEngine;
using DG.Tweening;

public class Stone : Enemy
{
    [SerializeField] private Vector3 _endPosition = new Vector3(0f, 0f, -5f);
    [SerializeField] private float _speed = 7f;
    
    private void Update()
    {
        Move();
    }

    public void AddEndPoint(Vector3 endPoint)
    {
        _endPosition.x = endPoint.x;
        _endPosition.y = endPoint.y;
    }
    
    protected override void Move()
    {
        // Sequence sequence = DOTween.Sequence();
        //
        // sequence.Append(transform.DORotate(new Vector3(360, 0, 0), 2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1));
        // sequence.Join(transform.DOMove(_endPosition, _speed).OnComplete(() =>
        // {
        //     sequence.Kill(true);
        //     gameObject.SetActive(false);
        // }));

        transform.position += Vector3.back * (_speed * Time.deltaTime);
        transform.Rotate(Vector3.right * (180f * Time.deltaTime));;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ActiveItem activeItem))
        {
            activeItem.gameObject.SetActive(false);
        }
    }
}
