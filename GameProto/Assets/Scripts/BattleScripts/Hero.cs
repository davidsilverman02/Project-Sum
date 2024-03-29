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

        strength.setPower(mang.attack.getEffective());
        magic.setPower(mang.magic.getEffective());
        defense.setPower(mang.defense.getEffective());
        wisdom.setPower(mang.wisdom.getEffective());
        agility.setPower(mang.agility.getEffective());
        speed.setPower(mang.speed.getEffective());

        calc.setWeaknesses(mang.weakness);

        powers = mang.skills;

        attack = mang.strike;
        defend = mang.defend;

        model = mang.model;

        GameObject ins = Instantiate(model, gameObject.transform.position, gameObject.transform.rotation);

        ins.transform.parent = gameObject.transform;

        bod = ins.GetComponent<UnitBody>();

        unitLevel = mang.level;
    }

    public StatContainer.StatObject getStats()
    {
        StatContainer.StatObject stats = new StatContainer.StatObject();

        stats.unitName = unitName;

        stats.currentHP = currentHP;
        stats.maxHP = maxHP;
        stats.currentPP = currentPP;
        stats.maxPP = maxPP;

        stats.attack.setSubtotal(unitLevel, true);
        stats.magic.setSubtotal(unitLevel, true);
        stats.defense.setSubtotal(unitLevel, true);
        stats.wisdom.setSubtotal(unitLevel, true);
        stats.agility.setSubtotal(unitLevel, true);
        stats.speed.setSubtotal(unitLevel, true);

        return stats;
    }
}
