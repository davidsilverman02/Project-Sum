using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatSave;
using WeaknessCalculator;

public class Hero : Unit
{
    public List<Skill> powers;

    public override void Start()
    {
        isPlayer = true;

        //base.Start();

        delay = FightMath.CounterSpeed(speed.get(), 0);

        currentTime = delay;

        ui.setMaxHP(maxHP);
        ui.SetHPBar(currentHP);

        ui.setMaxPP(maxPP);
        ui.setPPBar(currentPP);
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

        calc.setWeaknesses(mang.weakness);

        powers = mang.skills;

        attack = mang.strike;
        defend = mang.defend;

        model = mang.model;

        GameObject ins = Instantiate(model, gameObject.transform.position, gameObject.transform.rotation);

        ins.transform.parent = gameObject.transform;

        bod = ins.GetComponent<UnitBody>();
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
