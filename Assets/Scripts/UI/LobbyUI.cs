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
        stageStartBtn = transform.Find("StageStartButton").GetComponent<Button>(); //스테이지 스타트 버튼 할당
        stageStartBtn.onClick.AddListener(OnClickStageStartButton); //스테이지 스타트 버튼을 누른 경우
        
        nextStageBtn = transform.Find("NextStageButton").GetComponent<Button>(); //다음 스테이지  버튼 할당
        nextStageBtn.onClick.AddListener(OnClickNextStageButton);

        prevStageBtn = transform.Find("PrevStageButton").GetComponent<Button>(); //이전 스테이지 버튼 할당
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

