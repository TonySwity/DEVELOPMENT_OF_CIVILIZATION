using System;
using System.Collections;
using UnityEngine;

public class CoinAdder : ObjectPool
{
    [SerializeField] private GameObject _coinPref;

    private Wallet _wallet;
    private Camera _camera;
    private float _leftOffset = 2.5f;
    private float _backOffsetPoint1 = 3f;
    private float _backOffsetPoint2 = 2f;
    
    public event Action Finished;

    public void Initialize(Wallet wallet)
    {
        _camera = Camera.main;
        _wallet = wallet;
        Finished += _wallet.AddMoney;

        for (int j = 0; j < Constants.CoinAdder.CoinCountForPool; j++)
        {
            Initialize(_coinPref);
        }
    }

    public void StartCoinToWallet(Vector3 positionCell)
    {
        StartCoroutine(AddScoreAnimation(positionCell, _wallet.transform.position));
    }

    private IEnumerator AddScoreAnimation(Vector3 cellPosition, Vector3 walletPosition)
    {
        if (TryGetObjectFromPool(out GameObject result))
        {
            Vector3 point0 = cellPosition;
            Vector3 point1 = point0 + Vector3.left * _leftOffset + Vector3.back * _backOffsetPoint1;
            Vector3 screenPositionWallet = new Vector3(walletPosition.x + Constants.CoinAdder.AdjustmentX, walletPosition.y + Constants.CoinAdder.AdjustmentY, -_camera.transform.position.z);
            Vector3 point3 = _camera.ScreenToWorldPoint(screenPositionWallet);
            Vector3 point2 = point3 + Vector3.back * _backOffsetPoint2;
            result.transform.position = point0;
            result.SetActive(true);

            for (float time = 0; time < 1f; time += Time.deltaTime)
            {
                result.transform.position = Bezier.GetPoint(point0, point1, point2, point3, time);
                yield return null;
            }

            result.SetActive(false);
            Finished?.Invoke();
        }
    }

    private void OnDisable()
    {
        Finished -= _wallet.AddMoney;
    }
}
