using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    public TextMeshProUGUI testTxt;
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); //ui manager ½ÇÇà

        testTxt = transform.Find("TestTxt").GetComponent<TextMeshProUGUI>();
    }

    public void SetUI(StageState state)
    {
        testTxt.text = $"{state}";
    }

}
