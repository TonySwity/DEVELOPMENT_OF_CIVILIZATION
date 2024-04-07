using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    private Button _actionButton;

    private void Start()
    {
        gameObject.AddComponent<Button>();
        _actionButton = GetComponent<Button>();
        _actionButton?.onClick.AddListener(GetAction);
    }

    protected virtual void GetAction(){

    }

    private void OnDestroy()
    {
        _actionButton?.onClick.RemoveAllListeners();
    }
}
