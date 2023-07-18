using UnityEngine;

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
        transform.position += Vector3.back * (_speed * Time.deltaTime);
        transform.Rotate(Vector3.right * (180f * Time.deltaTime));;
    }
}
