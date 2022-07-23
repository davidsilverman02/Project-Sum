using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaknessCalculator;

public class Unit : MonoBehaviour
{
    public bool isPlayer;
    public bool hasTurn;
    public bool isDead;

    public string unitName;
    public int unitLevel;

    // Individual Stats for a unit
    public Stat strength;
    public Stat magic;
    public Stat defense;
    public Stat speed;

    public int currentHP;
    public int maxHP;
    public int currentPP;
    public int maxPP;

    public Weakness calc = new Weakness();

    public int delay;
    public int currentTime;

    public UnitUI ui;

    public Skill attack;
    public Skill defend;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // this will be replaced 
        delay = FightMath.CounterSpeed(speed.get(), 0);

        currentHP = maxHP;
        currentTime = delay;

        ui.setMaxHP(maxHP);

        ui.SetHPBar(currentHP);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        if(currentPP > maxPP)
        {
            currentPP = maxPP;
        }

        if(currentHP <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }

        DelayNum(currentTime);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageDisplay(damage, 0.2f));

        currentHP -= damage;

        if(currentHP < 0)
        {
            currentHP = 0;
        }

        setHealthBar();
    }

    public void Restore(int strength)
    {
        StartCoroutine(DamageDisplay(strength, 0.2f));

        currentHP += strength;

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        setHealthBar();
    }

    public bool Player()
    {
        return isPlayer;
    }

    public bool Dead()
    {
        return isDead;
    }

    public bool CanFight()
    {
        if(isDead)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Die(bool isDie)
    {
        isDead = true;
    }

    public int getStrength()
    {
        return strength.get();
    }

    public void buffAttack(int tier, int delay)
    {
        Vector2Int vec = new Vector2Int(tier, delay);
        strength.addBuff(vec);
    }

    public int getMagic()
    {
        return magic.get();
    }

    public void buffMagic(int tier, int delay)
    {
        Vector2Int vec = new Vector2Int(tier, delay);
        magic.addBuff(vec);
    }

    public int getDefense()
    {
        return defense.get();
    }

    public void buffDefense(int tier, int delay)
    {
        Vector2Int vec = new Vector2Int(tier, delay);
        defense.addBuff(vec);
    }

    public int getSpeed()
    {
        return speed.get();
    }

    public void buffSpeed(int tier, int delay)
    {
        Vector2Int vec = new Vector2Int(tier, delay);
        speed.addBuff(vec);
    }

    public void countBack(int tickBack)
    {
        currentTime -= tickBack;
        strength.countBack(tickBack);
        magic.countBack(tickBack);
        defense.countBack(tickBack);
        speed.countBack(tickBack);
    }

    public void resetTime()
    {
        //delay = FightMath.CounterSpeed(speed.get(), 0);
        currentTime = delay;
    }

    public void setDelay(int spd)
    {
        delay = spd;
    }

    public void setForTurn(int attackRank)
    {
        delay = FightMath.CounterSpeed(speed.get(), attackRank);
    }

    public int getDelay()
    {
        return delay;
    }

    public int getTime()
    {
        return currentTime;
    }

    public void setCurrent(int newTime)
    {
        currentTime = newTime;
    }

    public string getName()
    {
        return unitName;
    }

    public void ToggleSelected(bool select)
    {
        ui.SetTarget(select);
    }

    public void ToggleDamage(bool damage)
    {
        ui.SetDamage(damage);
    }

    public void DamageNum(int setNum)
    {
        ui.DamageLevel(setNum);
    }

    public void ColorDamage(Color color)
    {
        ui.DamageColor(color);
    }

    public void ToggleDelay(bool delay)
    {
        ui.SetDelay(delay);
    }

    public void DelayNum(int setNum)
    {
        ui.DelayLevel(setNum);
    }

    public IEnumerator DamageDisplay(int damage, float time)
    {
        DamageNum(damage);

        //Maybe change color

        ToggleDamage(true);

        yield return new WaitForSeconds(time);

        ToggleDamage(false);
    }

    public void setHealthBar()
    {
        ui.SetHPBar(currentHP);
    }

    public void runDamage(int raw, DamageType element, Unit opponent, bool drain)
    {
        calc.effectiveDamage(raw, element, opponent, this, drain);
    }
}
