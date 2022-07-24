using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedSkills : ScriptableObject
{
    public Skill newAttack;
    public Skill newDefense;

    public List<Skill> gainedSkills;
}
