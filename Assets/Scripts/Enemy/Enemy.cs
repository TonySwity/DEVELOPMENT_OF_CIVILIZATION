using UnityEngine;

public class Enemy: MonoBehaviour
{
    [field: SerializeField]public AgeItem AgeItem { get; private set; }

    protected virtual void Move() {}
}
