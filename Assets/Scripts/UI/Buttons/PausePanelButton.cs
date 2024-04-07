using UnityEngine;

public class PausePanelButton : BaseButton
{
    [SerializeField]private PausePanel _pausePanel;
    // Start is called before the first frame update
    protected override void GetAction()
    {
         _pausePanel?.gameObject.SetActive(true);
    }
}
