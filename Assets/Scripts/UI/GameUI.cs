using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    public TextMeshProUGUI stageTxt;
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); //ui manager ½ÇÇà

        stageTxt = transform.Find("StageTxt").GetComponent<TextMeshProUGUI>();
    }

    public void SetStageUI(StageState state)
    {
        stageTxt.text = $"{state}";
    }

}
