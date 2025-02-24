using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public List<SkillInfo> skillTable;
    public List<SkillInfo> playerSkillList = new List<SkillInfo>();

    private const int maxHP = 600;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        // �׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkillInfo randSkill = GetRandomSkill();
            //SkillGacha();
        }
    }

    // ������ ��ų 3���� ����Ʈ�� ��ȯ
    public List<SkillInfo> SkillGacha()
    {
        List<SkillInfo> rndSkillList = new List<SkillInfo>();

        SkillInfo rndSkill = new SkillInfo();

        for (int i = 0; i < 3; i++)
        {
            rndSkill = skillTable[(Random.Range(0, skillTable.Count))];

            if(!rndSkillList.Contains(rndSkill))
            {
                rndSkillList.Add(rndSkill);
            }
            else
            {
                do
                {
                    rndSkill = skillTable[(Random.Range(0, skillTable.Count))];
                }
                while (rndSkillList.Contains(rndSkill));

                rndSkillList.Add(rndSkill);
            }
        }

        Debug.Log($"{rndSkillList[0].SkillName}, {rndSkillList[1].SkillName}, {rndSkillList[2].SkillName},");

        return rndSkillList;
    }

    // �׽�Ʈ ��
    public SkillInfo GetRandomSkill()
    {
        int randomNum = Random.Range(0, skillTable.Count);

        SkillInfo skill = skillTable[randomNum];

        // ��ȹ�� �Ұ��� ��ų�̶�� ��ų���̺��� ����
        if (!skill.IsReacquirable)
            skillTable.RemoveAt(randomNum);

        // ��ų üũ�� �ӽ�
        playerSkillList.Add(skill);
        ApplySkill(skill);

        return skill;
    }

    // ��ų �����ϱ�
    public void ApplySkill(SkillInfo info)
    {
        switch (info.SkillID)
        {
            case Skill.DamageBoost:
                PlayerManager.instance.bow.Damage += (int)(PlayerManager.instance.bow.Damage * 0.3f);
                break;

            case Skill.AttackSpeedBoost:
                PlayerManager.instance.bow.IncreasedAttackSpeed(0.25f);
                break;

            case Skill.CriticalMaster:
                PlayerManager.instance.bow.CriticalChance += 10;
                PlayerManager.instance.bow.CriticalDamage += 0.4f;
                PlayerManager.instance.bow.CriticalChance = Mathf.Min(100, PlayerManager.instance.bow.CriticalChance);
                break;

            case Skill.HealthBoost:
                int incresedHealth = (int)(maxHP * 0.2);
                PlayerManager.instance.stats.MaxHealth += incresedHealth;
                PlayerManager.instance.stats.CurrentHealth += incresedHealth;
                break;

            case Skill.DodgeMastery:
                PlayerManager.instance.stats.DodgeChance += 20;
                PlayerManager.instance.stats.DodgeChance = Mathf.Min(100, PlayerManager.instance.stats.DodgeChance);
                break;

            case Skill.Invincibility:
                StartCoroutine(PlayerManager.instance.stats.ApplyInvincibilitySkill());
                break;

            case Skill.Rebound:
                PlayerManager.instance.bow.IsRebound = true;
                break;

            case Skill.PiercingShot:
                PlayerManager.instance.bow.IsPiercingShot = true;
                break;

            case Skill.FrontArrowPlus:
                PlayerManager.instance.arrowManager.AddShootDelegate(info.SkillID);
                break;

            case Skill.BackArrowPlus:
                PlayerManager.instance.arrowManager.AddShootDelegate(info.SkillID);
                break;

            case Skill.DiagonalArrowPlus:
                PlayerManager.instance.arrowManager.AddShootDelegate(info.SkillID);
                break;

            case Skill.SideArrowPlus:
                PlayerManager.instance.arrowManager.AddShootDelegate(info.SkillID);
                break;

            case Skill.MultiShot:
                PlayerManager.instance.bow.IsMultiShot = true;
                PlayerManager.instance.bow.IncreasedAttackSpeed(-0.15f);
                break;

            default:
                Debug.Log("���� ���� X");
                break;
        }
    }
}
