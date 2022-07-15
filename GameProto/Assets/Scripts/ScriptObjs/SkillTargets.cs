using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
