using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    public TextMeshProUGUI stageTxt;
    public GameObject hpBar;
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); //ui manager ½ÇÇà

        stageTxt = transform.Find("StageTxt").GetComponent<TextMeshProUGUI>();
        hpBar = transform.Find("PlayerBar").gameObject;
    }

    public void SetStageUI(StageState state)
    {
        stageTxt.text = $"{state}";
    }

    public void SetPlayerHPUIPosition(Vector2 pos)
    {
        hpBar.transform.position = Camera.main.WorldToScreenPoint(pos + new Vector2(0, -0.7f));
    }

}
