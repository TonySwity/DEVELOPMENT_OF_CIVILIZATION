using UnityEngine;

public class Disactivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.DisActivate();
        }
    }
}

