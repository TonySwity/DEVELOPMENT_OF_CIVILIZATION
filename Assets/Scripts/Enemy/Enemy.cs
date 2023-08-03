using UnityEngine;

public class Enemy: MonoBehaviour
{
    [field: SerializeField]public AgeItem AgeItem { get; private set; }
    
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public void GetFromPool()
    {
        gameObject.SetActive(true);
    }
    
    protected virtual void Move() {}
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ActiveItem activeItem))
        {
            activeItem.ReturnToPool();
        }
    }
}
