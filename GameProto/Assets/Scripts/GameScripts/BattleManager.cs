using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE }
public enum Target { ONE}
public enum PlayerCommand { ATTACK}

public class BattleManager : MonoBehaviour
{
    const int POOL_NUM = 1;
    const int PLAYERS = 1;
    const int PLAYER_UNITS = 4;
    const int ENEMY_UNITS = 4;
    const int ALL = 8;

    public int selectedUnit;
    public int choce;

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
    public PlayerCommand maneuver;
    public Unit currentUnit;
    public Unit selectOne;
    public bool choose;
    public bool onEnemy;
    public bool turnEnd;

    public GameObject[] enemyPool = new GameObject[POOL_NUM];
    public GameObject[] playerPool = new GameObject[PLAYERS];

    public GameObject[] selectPool = new GameObject[ALL];

    public List<Unit> playerOrder;

    public List<Unit> selected;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        ui = FindObjectOfType<BattleUIManager>();
        state = BattleState.START;
        SetupBattle(1);
        StartCoroutine(StartBattle());

        foreach(GameObject selector in selectPool)
        {
            selector.SetActive(false);
        }
    }


    // Runs items not run by the interfaces
    private void FixedUpdate()
    {
        if(state == BattleState.PLAYERTURN)
        {
            if (choose == false)
            {
                ui.TogglePlayer(true);
            }
            else
            {

            }
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
        playerOrder.Clear();

        foreach(Unit unit in players)
        {
            playerOrder.Add(unit);
        }

        foreach (Unit unit in monsters)
        {
            playerOrder.Add(unit);
        }

        state = BattleState.PLAYERTURN;
    }
    
    // Has an object engaging in battle
    void unitTurn()
    {

    }

    // Has the player select which unit is being targetted in battle
    void select()
    {
        choose = true;

        switch(choice)
        {
            case Target.ONE:
                selectPool[0].SetActive(true);
                break;
        }
    }

    int seal()
    {
        if(onEnemy)
        {
            choce = monsters.Count;
        }
        else
        {
            choce = players.Count;
        }

        if(selectedUnit < 0)
        {
            return choce - 1;
        }
        else if(selectedUnit > choce - 1)
        {
            return 0;
        }
        else
        {
            return selectedUnit;
        }
    }

    void oneSelect()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedUnit = seal();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedUnit = seal();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            onEnemy = !onEnemy;
            selectedUnit = seal();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            onEnemy = !onEnemy;
            selectedUnit = seal();
        }
    }

    // The script for the player attack
    void playerAttack()
    {
        select();
    }

    // Has an opponent take damage
    void dealDamage(int damage, Unit target)
    {

    }
}
