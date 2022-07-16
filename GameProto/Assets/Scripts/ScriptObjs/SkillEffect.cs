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
    public bool appliesDefend;

    public Vector3Int buffsAttack;
    public Vector3Int buffsMagic;
    public Vector3Int buffsDefense;
    public Vector3Int buffsSpeed;

    public virtual void Execute(Unit[] targets)
    {

    }
}
