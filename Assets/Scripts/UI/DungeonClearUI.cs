using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonClearUI : BaseUI
{
    public TextMeshProUGUI clearTimeTxt;
    public TextMeshProUGUI levelScoreTxt;
    public Button backButton;
    protected override UIState GetUIState()
    {
        return UIState.DungeonClear;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        clearTimeTxt = transform.Find("ClearTimeTxt").GetComponent<TextMeshProUGUI>();
        levelScoreTxt = transform.Find("LevelScoreTxt").GetComponent<TextMeshProUGUI>();
        backButton = transform.Find("DungeonClearButton").GetComponent<Button>();
        backButton.onClick.AddListener(OnClickBack);

    }

    public void UpdateScoreBoard(float time, int level)
    {
        Time.timeScale = 0;

        string temp = time.ToString("F1");
        clearTimeTxt.text = $"Clear Time : {temp}";
        levelScoreTxt.text = $"Level Score : {level}";
    }

    public void OnClickBack()
    {
        Time.timeScale = 1;
        uiManager.BackToLobby();
    }
}
