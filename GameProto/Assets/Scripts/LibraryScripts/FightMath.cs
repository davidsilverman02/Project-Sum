using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMath
{
    public enum Option { NOTHING, ATTACK, SKILL, DEFEND, ITEM, FLEE}

    public static int speedRank(int speed)
    {
        return Mathf.RoundToInt(((float)(100 - speed)) / 4.0f) + 3;
    }

    public static float moveRank(int priority)
    {
        return (float)priority;
    }

    public static float preventUnderLoad(float filtered)
    {
        if(filtered >= 1f)
        {
            return filtered;
        }
        else
        {
            return 1f;
        }
    }

    public static int CounterSpeed(int rank, int speed)
    {
        return Mathf.RoundToInt((float)speedRank(rank) - moveRank(speed));
    }

    public static void CalculateDamage(Skill skill, Unit user, Unit target, int effect, bool drain)
    {
        int calculated;
        int baseDamage;

        if (skill.physical)
        {
            calculated = Mathf.RoundToInt(skill.targets[effect].GetEffect().damage * (float)user.getStrength());
        }
        else
        {
            calculated = Mathf.RoundToInt(skill.targets[effect].GetEffect().damage * (float)user.getMagic());
        }

        baseDamage = calculated * (100 / 100 + target.getDefense());

        target.runDamage(baseDamage, skill.targets[effect].GetEffect().kind, user, drain);
    }

    public static void CalculateHealing(Skill skill, Unit user, Unit target, int effect)
    {
        int calculated;

        if (skill.physical)
        {
            calculated = Mathf.RoundToInt(skill.targets[effect].GetEffect().healing * (float)user.getStrength());
        }
        else
        {
            calculated = Mathf.RoundToInt(skill.targets[effect].GetEffect().healing * (float)user.getMagic());
        }

        target.Restore(calculated);
    }
}
