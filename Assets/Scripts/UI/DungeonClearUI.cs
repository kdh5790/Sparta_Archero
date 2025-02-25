using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonClearUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.DungeonClear;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
}
