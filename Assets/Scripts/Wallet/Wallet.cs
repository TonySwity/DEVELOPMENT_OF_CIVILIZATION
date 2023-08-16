using System;
using TMPro;
using UnityEngine;

public class Wallet: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _walletText;
    [SerializeField] private TextMeshProUGUI _incomePriceText;
    [SerializeField] private TextMeshProUGUI _activeItemText;
    [SerializeField] private TextMeshProUGUI _levelUpText;
    [SerializeField] private TextMeshProUGUI _levelUpPriceText;
    [SerializeField] private float _timeBetweenIncome = 3f;
    [SerializeField] private int _value = 20;

    private float _timer = 0f;
    private int _income = 1;
    private int _incomePrice = 40;
    private int _levelUpPrice = 1000;
    private int _levelIndex = 0;
    
    public event Action<int> LevelUpped;
    public event Action AddedMoney;
    
    public void Initialize()
    {
        _walletText.text = _value.ToString();
        _incomePriceText.text = _incomePrice + Constants.Wallet.MoneySymbol;
        _activeItemText.text = Constants.Wallet.ActiveItemPrice + Constants.Wallet.MoneySymbol;
        _levelUpText.text = _levelIndex.ToString();
        _levelUpPriceText.text = _levelUpPrice + Constants.Wallet.MoneySymbol;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeBetweenIncome)
        {
            AddMoney();
            _timer = 0f;
            AddedMoney?.Invoke();
        }
    }
    
    public void BuyLevelUp()
    {
        if (TryBuy(_levelUpPrice))
        {
            _levelIndex++;
            _levelUpPrice *= Constants.Wallet.MultiplicationFactorLevelUp;
            _levelUpText.text = _levelIndex.ToString();
            _levelUpPriceText.text = _levelUpPrice + Constants.Wallet.MoneySymbol;
            LevelUpped?.Invoke(_levelIndex);
        }
    }
    
    public bool TryBuy()
    {
        if (_value < Constants.Wallet.ActiveItemPrice)
        {
            return false;
        }
        
        _value -= Constants.Wallet.ActiveItemPrice;
        _walletText.text = _value.ToString();
        return true;
    }

    public void IncreasePriseIncome()
    {
        if (TryBuy(_incomePrice) == false)
        {
            return;
        }
        
        AddIncome();
        _incomePrice += _incomePrice;
        _incomePriceText.text = _incomePrice + Constants.Wallet.MoneySymbol;
    }

    public void AddMoneyWhenNextActiveItemSpawn()
    {
        _value += Constants.Wallet.IncomeNextActiveItemSpawn;
        _walletText.text = _value.ToString();
    }
    
    public void AddMoney()
    {
        _value += _income;
        _walletText.text = _value.ToString();
    }

    private bool TryBuy(int value)
    {
        
        if (_value < value)
        {
            return false;
        }
        
        _value -= value;
        _walletText.text = _value.ToString();
        
        return true;
    }
    
    private void AddIncome()
    {
        _income += Constants.Wallet.IncomeIncrease;
    }
}

