using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "Skill/Skill Effect")]
public class SkillEffect : ScriptableObject
{
    public float damage;
    public float healing;

    public bool appliesSlow;

    public bool buffsAttack;
    public bool buffsMagic;
    public bool buffsDefense;
    public bool buffsSpeed;

    public virtual void Execute(Unit[] targets)
    {

    }
}
