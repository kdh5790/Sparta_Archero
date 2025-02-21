using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public List<SkillInfo> skillInfoList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
