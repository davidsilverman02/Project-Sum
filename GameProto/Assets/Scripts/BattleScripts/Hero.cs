using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    public List<FightMath.Option> opts;

    public List<FightMath.Option> GetOptions()
    {
        return opts;
    }
}
