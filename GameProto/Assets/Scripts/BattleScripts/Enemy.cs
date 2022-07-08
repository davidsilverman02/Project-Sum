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
    public Unit choosing;
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
        StartCoroutine(BaseSkill());
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

    IEnumerator BaseSkill()
    {
        man.attackTalk("Attack");

        priority = 1;

        yield return new WaitForSeconds(0.5f);

        man.dealDamage(strength, selectLivingPlayer());

        yield return new WaitForSeconds(0.5f);

        CompleteTurn();
    }

    public virtual Unit getRandomPlayer()
    {
        int size = man.getPlayers().Count;

        return man.getPlayers()[Random.Range(0, size - 1)];
    }

    public virtual Unit getRandomEnemy()
    {
        int size = man.getEnemies().Count;

        return man.getEnemies()[Random.Range(0, size - 1)];
    }

    public virtual Unit getRandom()
    {
        int size = man.getAll().Count;

        return man.getAll()[Random.Range(0, size - 1)];
    }

    public Unit selectLivingPlayer()
    {
        do
        {
            choosing = getRandomPlayer(); 
        } while (choosing.Dead());

        return choosing;
    }
}
