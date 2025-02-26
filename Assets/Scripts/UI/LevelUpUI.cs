using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : BaseUI
{
    List<SkillInfo> skillInfos; //랜덤 스킬 선택용 리스트

    Button skill1Btn = null;
    public TextMeshProUGUI skill1NameTxt;
    public TextMeshProUGUI skill1DescriptionTxt;

    Button skill2Btn = null;
    public TextMeshProUGUI skill2NameTxt;
    public TextMeshProUGUI skill2DescriptionTxt;

    Button skill3Btn = null;
    public TextMeshProUGUI skill3NameTxt;
    public TextMeshProUGUI skill3DescriptionTxt;


    protected override UIState GetUIState()
    {
        return UIState.LevelUp;
    }

    public override void Init(UIManager uiManager)
    {
        skill1Btn = transform.Find("Skill1").GetComponent<Button>();
        skill1Btn.onClick.AddListener(OnClickSkill1);
        skill2Btn = transform.Find("Skill2").GetComponent<Button>();
        skill2Btn.onClick.AddListener(OnClickSkill2);
        skill3Btn = transform.Find("Skill3").GetComponent<Button>();
        skill3Btn.onClick.AddListener(OnClickSkill3);

        base.Init(uiManager);

    }

    public void SkillSelectOn() //스킬 선택창에 랜덤 스킬 배분
    {
        Time.timeScale = 0;

        skillInfos = SkillManager.instance.SkillGacha();

        skill1NameTxt.text = $"{skillInfos[0].SkillName}";
        skill1DescriptionTxt.text = $"{skillInfos[0].SkillDescription}";

        skill2NameTxt.text = $"{skillInfos[1].SkillName}";
        skill2DescriptionTxt.text = $"{skillInfos[1].SkillDescription}";

        skill3NameTxt.text = $"{skillInfos[2].SkillName}";
        skill3DescriptionTxt.text = $"{skillInfos[2].SkillDescription}";

    }

    public void OnClickSkill1()
    {
        SkillManager.instance.ApplySkill(skillInfos[0]); //스킬 적용

        Time.timeScale = 1;
        uiManager.OnClickSkillSelected();
    }

    public void OnClickSkill2()
    {
        SkillManager.instance.ApplySkill(skillInfos[1]); //스킬 적용

        Time.timeScale = 1;
        uiManager.OnClickSkillSelected();
    }

    public void OnClickSkill3()
    {
        SkillManager.instance.ApplySkill(skillInfos[2]); //스킬 적용

        Time.timeScale = 1;
        uiManager.OnClickSkillSelected();
    }



}
