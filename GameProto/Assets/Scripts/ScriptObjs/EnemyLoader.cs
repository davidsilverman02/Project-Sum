using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaknessCalculator;

[CreateAssetMenu(fileName = "EnemyLoader", menuName = "EnemyLoader")]
public class EnemyLoader : ScriptableObject
{
    public string unitName;
    public int unitLevel;

    // Individual Stats for a unit
    public int strength;
    public int magic;
    public int defense;
    public int speed;

    public int maxHP;
    public int maxPP;

    public Weakness calc;

    public FightMath.EnemyAI ai;

    public GameObject model;
}
