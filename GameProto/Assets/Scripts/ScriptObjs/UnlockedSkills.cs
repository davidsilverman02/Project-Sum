using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatSave;

[System.Serializable]
public class UnlockedSkills
{
    public Skill newAttack;
    public Skill newDefense;

    public List<Skill> gainedSkills;

    public int moreHP;
    public int morePP;

    public int attackIncrease;
    public int magicIncrease;
    public int defenseIncrease;
    public int speedIncrease;

    public UnlockedSkills()
    {
        
    }

    public void upgradeUnit(StatContainer.StatObject stObj)
    {
        if(newAttack != null)
        {
            stObj.strike = newAttack;
        }

        if(newDefense != null)
        {
            stObj.defend = newDefense;
        }

        if(gainedSkills.Count > 0)
        {
            for(int i = 0; i < gainedSkills.Count; i++)
            {
                stObj.skills.Add(gainedSkills[i]);
            }
        }

        if(moreHP > 0)
        {
            stObj.maxHP += moreHP;
            stObj.currentHP += moreHP;
        }

        if(morePP > 0)
        {
            stObj.maxPP += morePP;
            stObj.currentPP += morePP;
        }

        /*
        if(attackIncrease > 0)
        {
            stObj.attack += attackIncrease;
        }

        if(magicIncrease > 0)
        {
            stObj.magic += magicIncrease;
        }

        if(defenseIncrease > 0)
        {
            stObj.defense += defenseIncrease;
        }

        if(speedIncrease > 0)
        {
            stObj.speed += speedIncrease;
        }
        */
    }
}
