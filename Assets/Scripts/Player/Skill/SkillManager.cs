using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public List<SkillInfo> skillTable;


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
            Debug.Log($"{randSkill.name} Ω∫≈≥¿ª »πµÊ«ﬂΩ¿¥œ¥Ÿ.");
        }
    }

    public SkillInfo GetRandomSkill()
    {
        int randomNum = Random.Range(0, skillTable.Count);

        SkillInfo skill = skillTable[randomNum];

        if (!skill.IsReacquirable)
            skillTable.RemoveAt(randomNum);

        return skill;
    }
}
