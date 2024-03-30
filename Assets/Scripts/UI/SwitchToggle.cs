using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private RectTransform _uiHandleRectTransform;

    private Toggle _toggle;
    
    private Vector2 _handlePosition;

    private int _setPosition = -1;

    private void OnEnable()
    {
        _toggle = GetComponent<Toggle>();
        _handlePosition = _uiHandleRectTransform.anchoredPosition;
        _toggle.onValueChanged.AddListener(OnSwitch);

        if (_toggle.isOn)
        {
            OnSwitch(true);
        }
    }

    private void OnSwitch(bool on)
    {
        _uiHandleRectTransform.anchoredPosition = on ? _handlePosition * _setPosition : _handlePosition;
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
