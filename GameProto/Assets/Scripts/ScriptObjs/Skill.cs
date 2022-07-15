using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill/Skill")]
public class Skill : ScriptableObject
{
    public string named;
    public bool physical;
    public int cost;
    public int priority;
    public float duration;

    public SkillTargets[] targets;
    
    
    public void Execute()
    {
        for(int i = 0; i < targets.Length; i++)
        {
            targets[i].Enact();
        }
    }
}
