using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Wallet: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _walletText;
    [SerializeField] private float _timeBetweenIncome = 3f;
    [field: SerializeField] public int Value { get; private set; } = 20;

    private float _timer = 0;
    private int _income = 1;

    private void Start()
    {
        _walletText.text = Value.ToString();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeBetweenIncome)
        {
            Value += _income;
            _timer = 0f;
            _walletText.text = Value.ToString();
        }
    }

    public void AddIncome(int value)
    {
        _income = value;
    }
    
    
}

