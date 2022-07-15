using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This Script Generates the Stats for enemies
 * 
 */
public class Enemy : Unit
{
    public BattleManager man;
    public int choosing;
    public int priority;

    public override void Start()
    {
        base.Start();
    }

    public void Awake()
    {
        man = FindObjectOfType<BattleManager>();
    }

    public virtual void Behavior()
    {
        TurnCalled();
        man.enemySelect(attack);
        choosing = selectLivingPlayer();
        Debug.Log(choosing);
        StartCoroutine(man.useSkill(attack, choosing));
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

        return Random.Range(0, size - 1);
    }

    public virtual int getRandomEnemy()
    {
        man.setTarget(true);

        int size = man.getEnemies().Count;

        return Random.Range(0, size - 1);
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
