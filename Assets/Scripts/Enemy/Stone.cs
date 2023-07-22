using UnityEngine;

public class Stone : Enemy
{
    [SerializeField] private float _speed = 7f;
    
    private void Update()
    {
        Move();
    }
    
    protected override void Move()
    {
        transform.position += Vector3.back * (_speed * Time.deltaTime);
        transform.Rotate(Vector3.right * (180f * Time.deltaTime));;
    }
}
