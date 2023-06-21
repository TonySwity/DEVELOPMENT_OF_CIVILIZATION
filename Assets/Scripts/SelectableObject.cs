using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public virtual void OnHover()
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public virtual void OnUnhover()
    {
        transform.localScale = Vector3.one;
    }
    
}
