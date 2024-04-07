using UnityEngine;

public class BuyNextLevelActiveItemButton : BaseButton
{
    [SerializeField] private  Wallet _wallet;

    protected override void GetAction()
    {
        _wallet.BuyLevelUp();
    }
}
