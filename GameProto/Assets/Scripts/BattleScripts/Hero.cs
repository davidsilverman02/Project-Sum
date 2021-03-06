using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatSave;

public class Hero : Unit
{
    public override void Start()
    {
        isPlayer = true;

        base.Start();
    }

    public void setStats(StatContainer.StatObject mang)
    {
        unitName = mang.unitName;

        currentHP = mang.currentHP;
        maxHP = mang.maxHP;
        currentPP = mang.currentPP;
        maxPP = mang.maxPP;

        strength.setPower(mang.attack);
        magic.setPower(mang.magic);
        defense.setPower(mang.defense);
        speed.setPower(mang.speed);
    }

    public StatContainer.StatObject getStats()
    {
        StatContainer.StatObject stats = new StatContainer.StatObject();

        stats.unitName = unitName;

        stats.currentHP = currentHP;
        stats.maxHP = maxHP;
        stats.currentPP = currentPP;
        stats.maxPP = maxPP;

        stats.attack = strength.get();
        stats.magic = magic.get();
        stats.defense = defense.get();
        stats.speed = speed.get();

        return stats;
    }
}
