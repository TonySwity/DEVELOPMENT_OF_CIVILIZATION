using UnityEngine;

public class IncomeButton : BaseButton
{
    [SerializeField] private ActiveItemSpawner _spawner;
    protected override void GetAction()
    {
        _spawner.IncreaseIncome();
    }
}
