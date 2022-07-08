using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isPlayer;
    public bool hasTurn;
    public bool isDead;

    public string unitName;
    public int unitLevel;

    // Individual Stats for a unit
    public int strength;
    public int magic;
    public int defense;
    public int speed;

    public int currentHP;
    public int maxHP;
    public int currentPP;
    public int maxPP;

    public int delay;
    public int currentTime;

    public UnitUI ui;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // this will be replaced 
        delay = FightMath.CounterSpeed(speed, 0);

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
        currentHP -= damage;

        if(currentHP < 0)
        {
            currentHP = 0;
        }

        setHealthBar();
    }

    public void Restore(int strength)
    {
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
        return strength;
    }

    public int getMagic()
    {
        return magic;
    }

    public int getDefense()
    {
        return defense;
    }

    public int getSpeed()
    {
        return speed;
    }

    public void countBack(int tickBack)
    {
        currentTime -= tickBack;
    }

    public void resetTime()
    {
        delay = FightMath.CounterSpeed(speed, 0);
        currentTime = delay;
    }

    public void setDelay(int spd)
    {
        delay = spd;
    }

    public void setForTurn(int attackRank)
    {
        delay = FightMath.CounterSpeed(speed, attackRank);
        currentTime = delay;
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

        ToggleDamage(true);

        yield return new WaitForSeconds(time);

        ToggleDamage(false);
    }

    public void setHealthBar()
    {
        ui.SetHPBar(currentHP);
    }
}
