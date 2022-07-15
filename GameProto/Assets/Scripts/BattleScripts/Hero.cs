using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    public override void Start()
    {
        isPlayer = true;

        base.Start();
    }

}
