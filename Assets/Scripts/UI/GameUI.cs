using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    public TextMeshProUGUI dungeonTxt;
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

        dungeonTxt = transform.Find("DungeonTxt").GetComponent<TextMeshProUGUI>();
        playerBar = transform.Find("PlayerBar").gameObject;
    }

    public void SetDungeonUI(DungeonState state)
    {
        dungeonTxt.text = $"{state}";

        playerBar = transform.Find("PlayerBar").gameObject;
        hpFront = transform.Find("PlayerBar").transform.Find("Hp").transform.Find("Front").GetComponent<RectTransform>();
        expFront = transform.Find("PlayerBar").transform.Find("Exp").transform.Find("Front").GetComponent<RectTransform>();
    }

    public void SetPlayerUIPosition(Vector2 pos)
    {
        playerBar.transform.position = Camera.main.WorldToScreenPoint(pos + new Vector2(0, -1f));
    }

    public void SetPlayerHpUI(float max, int current)
    {
        if (hpFront != null)
        {
            hpFront.localScale = new Vector3(current / max, 1.0f, 1.0f);
            //깎인 Hp만큼 반영
        }
    }

    public void SetPlayerExpUI(float max, float current)
    {
        if (expFront != null)
        {
            expFront.localScale = new Vector3(current / max, 1.0f, 1.0f);
        }
    }

}
