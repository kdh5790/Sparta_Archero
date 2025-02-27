using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    Button dungeonStartBtn;
    Button nextDungeonBtn;
    Button prevDungeonBtn;
    Button changeColorBtn;
    Button titleBtn;
    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }

    public override void Init(UIManager uiManager)
    {
        dungeonStartBtn = transform.Find("DungeonStartButton").GetComponent<Button>(); //스테이지 스타트 버튼 할당
        dungeonStartBtn.onClick.AddListener(OnClickDungeonStartButton); //스테이지 스타트 버튼을 누른 경우

        titleBtn = transform.Find("TitleButton").GetComponent<Button>();
        titleBtn.onClick.AddListener(OnClickTitleButton);

        nextDungeonBtn = transform.Find("NextDungeonButton").GetComponent<Button>(); //다음 스테이지  버튼 할당
        nextDungeonBtn.onClick.AddListener(OnClickNextDungeonButton);

        prevDungeonBtn = transform.Find("PrevDungeonButton").GetComponent<Button>(); //이전 스테이지 버튼 할당
        prevDungeonBtn.onClick.AddListener(OnClickPrevDungeonButton);

        changeColorBtn = transform.Find("ChangeColorButton").GetComponent<Button>();
        changeColorBtn.onClick.AddListener(OnClickChangeColorButton);

        base.Init(uiManager);

    }

    public void OnClickDungeonStartButton()
    {
        uiManager.OnClickDungeonStart();
    }

    public void OnClickTitleButton()
    {
        uiManager.OnClickTitle();
    }

    public void OnClickNextDungeonButton()
    {
        uiManager.OnClickNextDungeon();
    }

    public void OnClickPrevDungeonButton()
    {
        uiManager.OnClickPrevDungeon();
    }

    public void OnClickChangeColorButton()
    {
        uiManager.OnClickChangeColor();
    }
}

