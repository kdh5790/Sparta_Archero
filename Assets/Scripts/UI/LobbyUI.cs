using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    Button stageStartBtn;
    Button nextStageBtn;
    Button prevStageBtn;
    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }

    public override void Init(UIManager uiManager)
    {
        stageStartBtn = transform.Find("StageStartButton").GetComponent<Button>(); //�������� ��ŸƮ ��ư �Ҵ�
        stageStartBtn.onClick.AddListener(OnClickStageStartButton); //�������� ��ŸƮ ��ư�� ���� ���
        
        nextStageBtn = transform.Find("NextStageButton").GetComponent<Button>(); //���� ��������  ��ư �Ҵ�
        nextStageBtn.onClick.AddListener(OnClickNextStageButton);

        prevStageBtn = transform.Find("PrevStageButton").GetComponent<Button>(); //���� �������� ��ư �Ҵ�
        prevStageBtn.onClick.AddListener(OnClickPrevStageButton);

        base.Init(uiManager);
    }

    public void OnClickStageStartButton()
    {
        uiManager.OnClickStageStart();
    }

    public void OnClickNextStageButton()
    {
        uiManager.OnClickNextStage();
    }

    public void OnClickPrevStageButton()
    {
        uiManager.OnClickPrevStage();
    }

}

