using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Targeted { Self, Other }

[System.Serializable]
public class SkillTargets
{
    public Target mode;

    public SkillEffect[] effects;

    private Unit[] targets;

    public void Enact()
    {
        EngageSkill();
    }

    void EngageSkill()
    {

    }
}
