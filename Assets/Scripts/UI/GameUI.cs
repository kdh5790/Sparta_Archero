using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    public TextMeshProUGUI stageTxt;
    public GameObject playerBar;

    public RectTransform hpFront; //�ش� ������ ����Ƽ���� �Ҵ�����
    public RectTransform expFront; //�ش� ������ ����Ƽ���� �Ҵ����� ��ũ��Ʈ ������Ʈ ����

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); //ui manager ����

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
        //���� Hp��ŭ �ݿ�
    }

    public void SetPlayerExpUI()
    {

    }

}
