using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    public List<FightMath.Option> opts;

    public override void Start()
    {
        isPlayer = true;

        base.Start();
    }

    public List<FightMath.Option> GetOptions()
    {
        return opts;
    }
}
