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
        stageStartBtn = transform.Find("StageStartButton").GetComponent<Button>(); //스테이지 스타트 버튼 할당
        stageStartBtn.onClick.AddListener(OnClickStageStartButton); //스테이지 스타트 버튼을 누른 경우

        base.Init(uiManager);
    }

    public void OnClickStageStartButton()
    {
        uiManager.OnClickStageStart();
    }

}

