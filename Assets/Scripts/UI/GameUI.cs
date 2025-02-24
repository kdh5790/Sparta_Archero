using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    public TextMeshProUGUI stageTxt;
    public GameObject playerBar;

    public RectTransform hpFront; //해당 변수는 유니티에서 할당해줌
    public RectTransform expFront; //해당 변수는 유니티에서 할당해줌 스크립트 컴포넌트 참고

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); //ui manager 실행

        stageTxt = transform.Find("StageTxt").GetComponent<TextMeshProUGUI>();
        playerBar = transform.Find("PlayerBar").gameObject;
    }

    public void SetStageUI(StageState state)
    {
        stageTxt.text = $"{state}";
    }

    public void SetPlayerUIPosition(Vector2 pos)
    {
        playerBar.transform.position = Camera.main.WorldToScreenPoint(pos + new Vector2(0, -0.7f));
    }

    public void SetPlayerHpUI(float max, int current)
    {
        hpFront.localScale = new Vector3(current / max, 1.0f,1.0f);
        //깎인 Hp만큼 반영
    }

    public void SetPlayerExpUI()
    {

    }

}
