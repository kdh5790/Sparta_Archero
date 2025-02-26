using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
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
    }

    public void OnClickBackSettingUI()
    {
        uiManager.OnClickBackSetting();
    }

}
