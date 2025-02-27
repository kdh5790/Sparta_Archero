using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button backBtn;
    protected override UIState GetUIState()
    {
        return UIState.Setting;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        backBtn = transform.Find("BackButton").GetComponent<Button>();
        backBtn.onClick.AddListener(OnClickBackSettingUI);

        bgmSlider.onValueChanged.AddListener(delegate { UpdateBGMVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { UpdateSFXVolume(); });
    }

    public void OnClickBackSettingUI()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickBackSetting();
    }

    private void UpdateBGMVolume()
    {
        SoundManager.instance.SetBGMVolume(bgmSlider.value);
    }   

    private void UpdateSFXVolume()
    {
        SoundManager.instance.SetSFXVolume(sfxSlider.value);
    }
}
