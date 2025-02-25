using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    Button gameOverBtn = null;
    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    public override void Init(UIManager uiManager)
    {

        base.Init(uiManager);

        gameOverBtn = transform.Find("GameOverButton").GetComponent<Button>();
        gameOverBtn.onClick.AddListener(OnClickBack);
    }

    public void OnClickBack()
    {
        uiManager.OnClickBackButton();
    }

}
