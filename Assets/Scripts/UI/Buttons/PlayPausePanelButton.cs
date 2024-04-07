using UnityEngine;

public class PlayPausePanelButton : BaseButton
{
    [SerializeField] private PausePanel _pausePanel;

    protected override void GetAction()
    {
        _pausePanel?.gameObject.SetActive(false);
    }
}