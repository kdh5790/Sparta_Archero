using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public List<SkillInfo> skillTable;
    public List<SkillInfo> playerSkillList = new List<SkillInfo>();

    private const int maxHP = 200;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkillInfo randSkill = GetRandomSkill();
        }
    }

    public SkillInfo GetRandomSkill()
    {
        int randomNum = Random.Range(0, skillTable.Count);

        SkillInfo skill = skillTable[randomNum];

        if (!skill.IsReacquirable)
            skillTable.RemoveAt(randomNum);

        playerSkillList.Add(skill);

        ApplySkill(skill.SkillID);

        return skill;
    }

    public void ApplySkill(Skill skill)
    {
        switch(skill)
        {
            case Skill.DamageBoost:
                PlayerManager.instance.bow.Damage += (int)(PlayerManager.instance.bow.Damage * 0.3f);
                break;

            case Skill.AttackSpeedBoost:
                PlayerManager.instance.bow.IncreasedAttackSpeed(0.25f);
                break;

            case Skill.CriticalMaster:
                PlayerManager.instance.bow.CriticalChance += 10;
                PlayerManager.instance.bow.CriticalDamage += 40;
                break;

            case Skill.HealthBoost:
                int incresedHealth = (int)(maxHP * 0.2);
                PlayerManager.instance.stats.MaxHealth += incresedHealth;
                PlayerManager.instance.stats.CurrentHealth += incresedHealth;
                break;

            case Skill.DodgeMastery:
                PlayerManager.instance.stats.dodgeChance += 20;
                break;

            default:
                Debug.Log("아직 구현 X");
                break;
        }
    }
}
