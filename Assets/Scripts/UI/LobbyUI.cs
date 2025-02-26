using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    Button dungeonStartBtn;
    Button nextDungeonBtn;
    Button prevDungeonBtn;
    Button ChangeColorBtn;

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }

    public override void Init(UIManager uiManager)
    {
        dungeonStartBtn = transform.Find("DungeonStartButton").GetComponent<Button>(); //�������� ��ŸƮ ��ư �Ҵ�
        dungeonStartBtn.onClick.AddListener(OnClickDungeonStartButton); //�������� ��ŸƮ ��ư�� ���� ���
        
        nextDungeonBtn = transform.Find("NextDungeonButton").GetComponent<Button>(); //���� ��������  ��ư �Ҵ�
        nextDungeonBtn.onClick.AddListener(OnClickNextDungeonButton);

        prevDungeonBtn = transform.Find("PrevDungeonButton").GetComponent<Button>(); //���� �������� ��ư �Ҵ�
        prevDungeonBtn.onClick.AddListener(OnClickPrevDungeonButton);

        ChangeColorBtn = transform.Find("ChangeColorButton").GetComponent<Button>(); // ���� ���� ��ư �Ҵ�
        ChangeColorBtn.onClick.AddListener(OnClickChangeColorButton);

        base.Init(uiManager);

    }

    public void OnClickDungeonStartButton()
    {
        uiManager.OnClickDungeonStart();
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

