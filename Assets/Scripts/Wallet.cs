using TMPro;
using UnityEngine;

public class Wallet: MonoBehaviour
{
    private const string MoneySymbol = "<color=green>$</color>";
    
    [SerializeField] private TextMeshProUGUI _incomePriceText;
    [SerializeField] private TextMeshProUGUI _walletText;
    [SerializeField] private TextMeshProUGUI _activeItemText;
    [SerializeField] private float _timeBetweenIncome = 3f;
    [SerializeField] private int _value = 20;

    private float _timer = 0f;
    private int _cellsCount = 0;
    
    private int _income = 1;
    private int _incomePrice = 40;
    private int _activeItemPrice = 17;
    private void Start()
    {
        _walletText.text = _value.ToString();
        _incomePriceText.text = _incomePrice + MoneySymbol;
        _activeItemText.text = _activeItemPrice + MoneySymbol;

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
            _cellsCount--;
        }
    }

    public bool TryBuy(int money)
    {
        if (_value < money)
        {
            return false;
        }
        
        _value -= money;
        _walletText.text = _value.ToString();
        return true;
    }
    
    public void AddIncome(int value)
    {
        _income += value;
    }

    public void IncreasePriseIncome(int money)
    {
        _incomePrice += money;
        _incomePriceText.text = _incomePrice + MoneySymbol;
    }
}

