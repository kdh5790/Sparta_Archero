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
        optionBtn = transform.Find("SettingButton").GetComponent<Button>();
        optionBtn.onClick.AddListener(OnClickSettingUI);
        lobbyBtn = transform.Find("LobbyButton").GetComponent<Button>();
        lobbyBtn.onClick.AddListener(OnClickLobbyUI);
    }

    public void OnClickContinueUI()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickContinue();
    }

    public void OnClickSettingUI()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickSetting();
    }

    public void OnClickLobbyUI()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        SoundManager.instance.PlayMusic(BGM.Lobby);
        uiManager.OnClickLobby();
    }

}