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

    public override void Start()
    {
        currentHP = maxHP;
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
        man.nextTurn();
    }

    // Put this as the first line in behavior
    public void TurnCalled()
    {
        man.turnCalled = true;
    }

    IEnumerator BaseSkill()
    {
        Debug.Log("yep");

        yield return new WaitForSeconds(1f);

        CompleteTurn();
    }
}
