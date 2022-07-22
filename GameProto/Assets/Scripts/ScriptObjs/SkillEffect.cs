using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaknessCalculator;

[System.Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "Skill/Skill Effect")]
public class SkillEffect : ScriptableObject
{
    public float damage;
    public float healing;

    public DamageType kind;

    public bool appliesSlow;
    public bool appliesDefend;

    public Vector2Int buffsAttack;
    public Vector2Int buffsMagic;
    public Vector2Int buffsDefense;
    public Vector2Int buffsSpeed;

    public virtual void Execute(Unit[] targets)
    {

    }
}
