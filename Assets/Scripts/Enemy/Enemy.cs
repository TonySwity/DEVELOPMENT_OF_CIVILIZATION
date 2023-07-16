using UnityEngine;

public class Enemy: MonoBehaviour
{
    public AgeItem AgeItem { get; private set; }

    protected virtual void Move() {}
}
