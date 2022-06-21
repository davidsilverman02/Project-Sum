using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE }
public enum Target { ONE}

public class BattleManager : MonoBehaviour
{
    const int POOL_NUM = 1;
    const int PLAYERS = 1;
    const int PLAYER_UNITS = 4;
    const int ENEMY_UNITS = 4;
    const int ALL = 8;

    public GameManager manager;
    public BattleUIManager ui;

    public Transform heroPos1;
    public Transform heroPos2;
    public Transform heroPos3;
    public Transform heroPos4;

    public Transform heroAim1;
    public Transform heroAim2;
    public Transform heroAim3;
    public Transform heroAim4;

    public Transform enemyPos1;
    public Transform enemyPos2;
    public Transform enemyPos3;
    public Transform enemyPos4;

    public Transform enemyAim1;
    public Transform enemyAim2;
    public Transform enemyAim3;
    public Transform enemyAim4;

    public List<Unit> players;
    public List<Unit> monsters;

    public BattleState state;
    public Target choice;
    public bool choose;

    public GameObject[] enemyPool = new GameObject[POOL_NUM];
    public GameObject[] playerPool = new GameObject[PLAYERS];

    public GameObject[] selectPool = new GameObject[ALL];

    public List<Unit> selected;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        ui = FindObjectOfType<BattleUIManager>();
        state = BattleState.START;
        SetupBattle(1);
        StartCoroutine(StartBattle());
    }


    // Runs items not run by the interfaces
    private void FixedUpdate()
    {
        if(state == BattleState.PLAYERTURN)
        {
            ui.TogglePlayer(true);
        }
        else
        {
            ui.TogglePlayer(false);
        }      
    }

    // Sets up the units in a battle
    void SetupBattle(int battleRandom)
    {
        //This statement generates random encounters
        if (true)
        {
            switch (battleRandom)
            {
                case 1:
                    setUnit(false, enemyPool[0], enemyPos1);
                    //setUnit(false, enemyPool[0], enemyPos2);
                    //setUnit(false, enemyPool[0], enemyPos3);
                    //setUnit(false, enemyPool[0], enemyPos4);

                    break;
                default:
                    break;
            }
            ui.SetBattleDescription("Monsters Approach!");
        }
        //This statement generates specific encounters
        else
        {

        }

        placeParty();
    }

    // This script places players on the folder
    void placeParty()
    {
        setUnit(true, playerPool[0], heroPos1);
        //setUnit(true, playerPool[0], heroPos2);
        //setUnit(true, playerPool[0], heroPos3);
        //setUnit(true, playerPool[0], heroPos4);
    }

    // Puts a unit in the battle
    void setUnit(bool player, GameObject unit, Transform pos)
    {
        GameObject spawn = Instantiate(unit, pos);

        if(player)
        {
            players.Add(spawn.GetComponent<Unit>());
        }
        else
        {
            monsters.Add(spawn.GetComponent<Unit>());
        }
    }

    // This script starts the battle
    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(1f);
        ui.ToggleOverhead(false);
        determineTurnOrder();
    }

    // Determines the fighters order in battle
    void determineTurnOrder()
    {
        state = BattleState.PLAYERTURN;
    }

    // Has the player select which unit is being targetted in battle
    void select()
    {
        choose = true;



        switch(choice)
        {
            case Target.ONE:
                break;
        }
    }

    // The script for the player attack
    void playerAttack()
    {
        
    }
}
