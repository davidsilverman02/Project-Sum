using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaknessCalculator;

[System.Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "Skill/Skill Target")]
public class SkillTargets : ScriptableObject
{
    public Target target;

    public SkillEffect effects;

    private List<List<bool>> list;

    public void Enact()
    {
        
    }

    public SkillEffect GetEffect()
    {
        return effects;
    }

    public DamageType damageKind()
    {
        return effects.kind;
    }
}
