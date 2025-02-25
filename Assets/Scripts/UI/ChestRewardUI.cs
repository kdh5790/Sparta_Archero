using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRewardUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.ChestReward;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
}
