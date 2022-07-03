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
    public int defense;
    public int speed;

    public int currentHP;
    public int maxHP;
    public int currentPP;
    public int maxPP;

    public UnitUI ui;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // this will be replaced 
        currentHP = maxHP;
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
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
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

    public int getDefense()
    {
        return defense;
    }

    public int getSpeed()
    {
        return speed;
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

    public IEnumerator DamageDisplay(int damage, float time)
    {
        DamageNum(damage);

        ToggleDamage(true);

        yield return new WaitForSeconds(time);

        ToggleDamage(false);
    }
}
