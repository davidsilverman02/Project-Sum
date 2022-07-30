using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This Script Generates the Stats for enemies
 * 
 */
public class Enemy : Unit
{
    public FightMath.EnemyAI ai;

    public BattleManager man;
    public int choosing;
    public int priority;

    public int exp;

    public EnemyLoader statLoader;

    public override void Start()
    {
        unitName = statLoader.unitName;
        unitLevel = statLoader.unitLevel;

        strength.setPower(statLoader.strength);
        magic.setPower(statLoader.magic);
        defense.setPower(statLoader.defense);
        wisdom.setPower(statLoader.wisdom);
        agility.setPower(statLoader.wisdom);
        speed.setPower(statLoader.speed);

        maxHP = statLoader.maxHP;
        maxPP = statLoader.maxPP;

        ai.LoadFromBase(statLoader.ai);

        model = statLoader.model;

        GameObject ins = Instantiate(model, gameObject.transform.position, gameObject.transform.rotation);

        ins.transform.parent = gameObject.transform;

        bod = ins.GetComponent<UnitBody>();

        exp = statLoader.exp;

        base.Start();
    }

    public void Awake()
    {
        man = FindObjectOfType<BattleManager>();
    }

    public virtual void Behavior()
    {
        TurnCalled();
        ai.selectTarget(man.getEnemies(), man.getPlayers());
        man.enemySelect(ai.getSkillUsing());
        setTarget(ai.target());
        StartCoroutine(man.useSkill(ai.getSkillUsing(), ai.getTarget()));

        /*
        man.enemySelect(attack);
        choosing = selectLivingPlayer();
        StartCoroutine(man.useSkill(attack, choosing));
        */
    }

    public virtual void ItemDrop()
    {

    }

    public void CompleteTurn()
    {
        man.disableTalk();

        man.setRank(priority);

        man.nextTurn();
    }

    // Put this as the first line in behavior
    public void TurnCalled()
    {
        man.turnCalled = true;
    }

    public virtual int getRandomPlayer()
    {
        man.setTarget(false);

        int size = man.getPlayers().Count;

        return Random.Range(0, size);
    }

    public virtual void setTarget(bool set)
    {
        man.setTarget(set);
    }

    public virtual int getRandomEnemy()
    {
        man.setTarget(true);

        int size = man.getEnemies().Count;

        return Random.Range(0, size);
    }

    public virtual int getRandom()
    {
        int differ = Random.Range(0, 1);

        if(differ > 0)
        {
            return getRandomPlayer();
        }
        else
        {
            return getRandomEnemy();
        }
    }

    public bool getEnemyDead(int index)
    {
        if(man.getEnemies()[index].Dead())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool getPlayerDead(int index)
    {
        if (man.getPlayers()[index].Dead())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
    public bool getDead()
    {
        if(man.getTarget())
        {
            if(man.getM)
        }
        else
        {

        }
    }
    */

    public int selectLivingPlayer()
    {
        int user;

        do
        {
            user = getRandomPlayer();
        } while (getPlayerDead(user));

        return user;
    }

    public override bool dodge(Unit opponent)
    {
        int baseProb = this.getAgility() - opponent.getAgility();

        probab = Mathf.Clamp(baseProb, 0, 100);

        randChanc = Random.Range(1, 100);

        if (randChanc <= probab)
            return true;
        else
            return false;
    }

    public override void TakeDamage(int damage)
    {
        StartCoroutine(DamageDisplay(damage, 0.2f));

        currentHP -= damage;

        if (currentHP <= 0)
        {
            currentHP = 0;
            man.addEXP(exp);
        }

        setHealthBar();
    }
}
