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

    public EnemyLoader statLoader;

    public override void Start()
    {
        unitName = statLoader.unitName;
        unitLevel = statLoader.unitLevel;

        strength.setPower(statLoader.strength);
        magic.setPower(statLoader.magic);
        defense.setPower(statLoader.defense);
        speed.setPower(statLoader.speed);

        maxHP = statLoader.maxHP;
        maxPP = statLoader.maxPP;

        ai.LoadFromBase(statLoader.ai);

        model = statLoader.model;

        GameObject ins = Instantiate(model, gameObject.transform.position, gameObject.transform.rotation);

        ins.transform.parent = gameObject.transform;

        bod = ins.GetComponent<UnitBody>();

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
}
