using UnityEngine;

public class Enemy: MonoBehaviour
{
    [field: SerializeField]public AgeItem AgeItem { get; private set; }
    
    public void DisActivate()
    {
        gameObject.SetActive(false);
    }
    
    protected virtual void Move() {}
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ActiveItem activeItem))
        {
            activeItem.gameObject.SetActive(false);
        }
    }
}
