using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    public Button continueBtn = null;
    public Button optionBtn = null;
    public Button lobbyBtn = null;
    protected override UIState GetUIState()
    {
        return UIState.Pause;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        continueBtn = transform.Find("ContinueButton").GetComponent<Button>();
        continueBtn.onClick.AddListener(OnClickContinueUI);
        optionBtn = transform.Find("OptionButton").GetComponent<Button>();
        optionBtn.onClick.AddListener(OnClickOptionUI);
        lobbyBtn = transform.Find("LobbyButton").GetComponent<Button>();
        lobbyBtn.onClick.AddListener(OnClickLobbyUI);
    }

    public void OnClickContinueUI()
    {
        uiManager.OnClickContinue();
    }

    public void OnClickOptionUI()
    {
        Debug.Log("¹Ì±¸Çö");
    }

    public void OnClickLobbyUI()
    {
        uiManager.OnClickLobby();
    }

}