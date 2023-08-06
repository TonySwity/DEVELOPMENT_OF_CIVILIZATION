using TMPro;
using UnityEngine;

public class Wallet: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _incomePriceText;
    [SerializeField] private TextMeshProUGUI _walletText;
    [SerializeField] private TextMeshProUGUI _activeItemText;
    [SerializeField] private float _timeBetweenIncome = 3f;
    [SerializeField] private int _value = 20;

    private float _timer = 0f;
    [SerializeField] private int _cellsCount = 0;
    private int _income = 1;
    private int _incomePrice = 40;
    private int _incomeNextActiveItemSpawn = 20;
    
    private void Start()
    {
        _walletText.text = _value.ToString();
        _incomePriceText.text = _incomePrice + Constants.Wallet.MoneySymbol;
        _activeItemText.text = Constants.Wallet.ActiveItemPrice + Constants.Wallet.MoneySymbol;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeBetweenIncome)
        {
            _value += _income + (_income * _cellsCount);
            _timer = 0f;
            _walletText.text = _value.ToString();
        }
    }

    public void SetCountCells(ItemType itemType)
    {
        if (itemType != ItemType.Empty)
        {
            _cellsCount++;
        }
        else
        {
            if (_cellsCount > 0)
            {
                _cellsCount--;
            }
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
        _value += _incomeNextActiveItemSpawn;
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

