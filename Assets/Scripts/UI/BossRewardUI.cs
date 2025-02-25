using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRewardUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.BossReward;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
}
