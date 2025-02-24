using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    Button stageStartBtn;
    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }

    public override void Init(UIManager uiManager)
    {
        stageStartBtn = transform.Find("StageStartButton").GetComponent<Button>(); //�������� ��ŸƮ ��ư �Ҵ�
        stageStartBtn.onClick.AddListener(OnClickStageStartButton); //�������� ��ŸƮ ��ư�� ���� ���

        base.Init(uiManager);
    }

    public void OnClickStageStartButton()
    {
        uiManager.OnClickStageStart();
    }

}

