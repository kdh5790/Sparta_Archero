using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    public GameObject changeColorObject;

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

        changeColorObject = transform.Find("ChangeColorButton").gameObject;

        changeColorBtn = transform.Find("ChangeColorButton").GetComponent<Button>();
        changeColorBtn.onClick.AddListener(OnClickChangeColorButton);

        base.Init(uiManager);

    }

    public void BoxAchievementCheck() //상자 업적 달성한 경우
    {
        changeColorObject.SetActive(true);
    }

    public void OnClickDungeonStartButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickDungeonStart();
    }

    public void OnClickTitleButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickTitle();
    }

    public void OnClickNextDungeonButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickNextDungeon();
    }

    public void OnClickPrevDungeonButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickPrevDungeon();
    }

    public void OnClickChangeColorButton()
    {
        SoundManager.instance.PlaySound(SFX.UIClick);
        uiManager.OnClickChangeColor();
    }
}

