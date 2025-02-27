using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : BaseUI
{
    Button startBtn;
    Button optionBtn;
    Button exitBtn;

    protected override UIState GetUIState()
    {
        return UIState.Title;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); //ui manager ½ÇÇà

        startBtn = transform.Find("StartButton").GetComponent<Button>();
        exitBtn = transform.Find("ExitButton").GetComponent<Button>();
        optionBtn = transform.Find("SettingButton").GetComponent<Button>();

        startBtn.onClick.AddListener(OnClickStartButton);
        exitBtn.onClick.AddListener(OnClickExitButton);
        optionBtn.onClick.AddListener(OnClickSettingButton);

    }


    public void OnClickStartButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickStart();
    }

    public void OnClickExitButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickExit();
    }

    public void OnClickSettingButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickSetting();
    }
}
