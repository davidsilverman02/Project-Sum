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

    public static int CalculateDamage(bool magic, int attackMod, int defense)
    {
        return attackMod * (100 / 100 + defense);
    }
}
