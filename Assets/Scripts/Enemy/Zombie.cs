using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _rotateSpeed = 180f;
    private void Update()
    {
        Move();
    }
    
    protected override void Move()
    {
        transform.position += Vector3.back * (_speed * Time.deltaTime);
        transform.Rotate(Vector3.right * (_rotateSpeed * Time.deltaTime));;
    }
}
