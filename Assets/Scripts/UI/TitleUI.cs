using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : BaseUI
{
    Button startBtn;
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

        startBtn.onClick.AddListener(OnClickStartButton);
        exitBtn.onClick.AddListener(OnClickExitButton);
    }


    public void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }

    public void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}
