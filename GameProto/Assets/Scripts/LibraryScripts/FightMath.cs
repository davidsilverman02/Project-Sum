using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMath
{
    public enum Option { NOTHING, ATTACK, LIFE }

    public static int speedRank(int speed)
    {
        return Mathf.RoundToInt(((float)(100 - speed)) / 4.0f) + 3;
    }

    public static float moveRank(int priority)
    {
        return 1.0f;
    }

    public static int CounterSpeed(int rank, int speed)
    {
        return Mathf.RoundToInt((float)speedRank(rank) * moveRank(speed));
    }
}
