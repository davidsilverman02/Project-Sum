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
    public int unitMoving;
    public int indexPoint;

    public GameManager manager;
    public BattleUIManager ui;

    public Transform heroPos1;
    public Transform heroPos2;
    public Transform heroPos3;
    public Transform heroPos4;

    public List<SelectRect> heroAim;

    public Transform enemyPos1;
    public Transform enemyPos2;
    public Transform enemyPos3;
    public Transform enemyPos4;

    public List<SelectRect> enemyAim;

    public List<Unit> players;
    public List<Unit> monsters;

    public BattleState state;
    public Target choice;
    public PlayerCommand maneuver;
    public Unit currentUnit;
    public Unit selectOne;
    public bool choose;
    public bool onEnemy;
    public bool turnCalled;
    public bool hasEnded;
    public bool executed;
    public GameObject[] enemyPool = new GameObject[POOL_NUM];
    public GameObject[] playerPool = new GameObject[PLAYERS];

    public List<Unit> playerOrder;

    public List<Unit> selected;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        ui = FindObjectOfType<BattleUIManager>();
        state = BattleState.START;
        SetupBattle(1);
        StartCoroutine(StartBattle());
        disableSelectors();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Runs items not run by the interfaces
    private void Update()
    {
        if(monsters.Count <= 0 || allDead(monsters))
        {
            state = BattleState.WIN;
        }
        else if(players.Count <= 0 || allDead(players))
        {
            state = BattleState.LOSE;
        }

        if(state == BattleState.WIN)
        {
            if(hasEnded == false)
            {
                StartCoroutine(WinMenu());
            }

            foreach(Unit unit in playerOrder)
            {
                unit.ToggleSelected(false);
            }
        }

        if(state == BattleState.LOSE)
        {
            if (hasEnded == false)
            {
                StartCoroutine(LoseMenu());
            }

            foreach (Unit unit in playerOrder)
            {
                unit.ToggleSelected(false);
            }
        }

        if(state == BattleState.PLAYERTURN || state == BattleState.ENEMYTURN)
        {
            state = stateIn(currentUnit);
        }

        if(state == BattleState.PLAYERTURN)
        {
            if (choose == false)
            {
                ui.TogglePlayer(true);

                disableSelectors();
            }
            else
            {
                
                ui.TogglePlayer(false);

                if (choice == Target.ONE)
                {

                    oneSelect();

                    if (onEnemy)
                    {
                        selectOne = monsters[selectedUnit];
                    }
                    else
                    {
                        selectOne = players[selectedUnit];
                    }
                }

                if(Input.GetKeyDown(KeyCode.Return))
                {
                    switch (maneuver)
                    {
                        case PlayerCommand.ATTACK:
                            StartCoroutine(playerAttack(selectOne));
                            break;
                    }
                    
                }
            }
        }
        else
        {
            ui.TogglePlayer(false);

            
        }
        
        if(state == BattleState.ENEMYTURN)
        {
            if(turnCalled == false)
            {
                currentUnit.GetComponent<Enemy>().Behavior();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    public int getPlayerCount()
    {
        return players.Count;
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
        setUnit(true, playerPool[0], heroPos2);
        setUnit(true, playerPool[0], heroPos3);
        setUnit(true, playerPool[0], heroPos4);
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
        currentUnit = playerOrder[unitMoving];
        state = stateIn(currentUnit);
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
            selectedUnit--;
            selectedUnit = seal();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedUnit++;
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

        foreach(Unit unit in playerOrder)
        {
            if(unit == selectOne)
            {
                unit.ToggleSelected(true);
            }
            else
            {
                unit.ToggleSelected(false);
            }
        }
    }

    // The script for the player attack
    public void playerAttackButton()
    {
        select();
    }

    // Has an opponent take damage
    void dealDamage(int damage, Unit target)
    {
        target.StartCoroutine(target.DamageDisplay(damage, 0.2f));

        target.TakeDamage(damage);
    }

    public IEnumerator playerAttack(Unit target)
    {
        dealDamage(currentUnit.getStrength(), target);

        yield return new WaitForSeconds(0f);

        choose = false;

        if (target.Dead() && target.Player() == false)
        {
            StartCoroutine(killEnemy(target));
        }
        else
        {
            nextTurn();
        }
        
    }
    
    
    public IEnumerator killEnemy(Unit target)
    {
        for (float i = 20f; i < 0; i--)
        {
            yield return new WaitForSeconds(0.05f);
        }

        RemoveUnit(target);

        nextTurn();
    }
    
    public void nextTurn()
    {
        
        do
        {
            if (unitMoving >= playerOrder.Count - 1)
            {
                unitMoving = 0;
            }
            else
            {
                unitMoving++;
            }
            
            currentUnit = playerOrder[unitMoving];
        } while (currentUnit.CanFight() == false);
        

        turnCalled = false;
    }

    public BattleState stateIn(Unit get)
    {
        if(get.Player())
        {
            return BattleState.PLAYERTURN;
        }
        else
        {
            return BattleState.ENEMYTURN;
        }
    }

    void disableSelectors()
    {
        foreach (Unit unit in playerOrder)
        {
            unit.ToggleSelected(false);
        }
    }

    void attackTalk(string words)
    {
        ui.ToggleOverhead(true);
        ui.SetBattleDescription(words);
    }

    //Physically removes a unit
    public void RemoveUnit(Unit toRemove)
    {
        if(toRemove.isPlayer)
        {
            indexPoint = players.IndexOf(toRemove);
            Destroy(heroAim[indexPoint].gameObject);
            heroAim.RemoveAt(indexPoint);
            players.Remove(toRemove);
        }
        else
        {
            Debug.Log("Hit");

            indexPoint = monsters.IndexOf(toRemove);
            Destroy(enemyAim[indexPoint].gameObject);
            enemyAim.RemoveAt(indexPoint);
            monsters.Remove(toRemove);
        }

        playerOrder.Remove(toRemove);

        Destroy(toRemove.gameObject);

        determineTurnOrder();

        if(currentUnit.Player())
        {

        }

        if (unitMoving >= playerOrder.Count - 1)
        {
            unitMoving = 0;
        }
    }

    public int findPlayerIndex()
    {
        return players.IndexOf(currentUnit);
    }

    // Sees if All objects in an object pool are dead
    public bool allDead(List<Unit> units)
    {
        foreach(Unit agent in units)
        {
            if(agent.Dead() == false)
            {
                return false;
            }
        }

        return true;
    }

    //merges for merge sort
    public void MergeUnits(List<Unit> unts, int lowerB, int mid, int upperB, int sort)
    {

    }

    public void UnitSort(List<Unit> unts, int lowerB, int mid, int upperB, int sort)
    {

    }

    void deselectUnits()
    {
        foreach(Unit unit in playerOrder)
        {
            unit.ui.SetTarget(false);
        }
    }

    IEnumerator WinMenu()
    {
        hasEnded = true;

        ui.SetBattleDescription("You Win!");
        ui.ToggleOverhead(true);

        yield return new WaitForSeconds(1f);

        manager.LoadBack();
    }

    IEnumerator LoseMenu()
    {
        hasEnded = true;

        ui.SetBattleDescription("You Lose!");
        ui.ToggleOverhead(true);

        yield return new WaitForSeconds(1f);

        manager.LoadLevel(3);
    }
}
