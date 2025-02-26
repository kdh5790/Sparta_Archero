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
        base.Init(uiManager); //ui manager 실행

        startBtn = transform.Find("StartButton").GetComponent<Button>();
        exitBtn = transform.Find("ExitButton").GetComponent<Button>();
        optionBtn = transform.Find("OptionButton").GetComponent<Button>();

        startBtn.onClick.AddListener(OnClickStartButton);
        exitBtn.onClick.AddListener(OnClickExitButton);
        optionBtn.onClick.AddListener(OnClickOptionButton);

    }


    public void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }

    public void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }

    public void OnClickOptionButton()
    {
        Debug.Log("미구현");
    }
}
