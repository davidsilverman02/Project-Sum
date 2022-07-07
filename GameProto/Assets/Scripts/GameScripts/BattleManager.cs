using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, CALCULATING, PLAYERTURN, ENEMYTURN, WIN, LOSE }
public enum Target { ONE}

public class BattleManager : MonoBehaviour
{
    const int POOL_NUM = 1;
    const int PLAYERS = 1;
    const int PLAYER_UNITS = 4;
    const int ENEMY_UNITS = 4;
    const int ALL = 8;

    public int battleSpeed = 1;
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

    public Transform enemyPos1;
    public Transform enemyPos2;
    public Transform enemyPos3;
    public Transform enemyPos4;

    public List<Unit> players;
    public List<Unit> monsters;

    public BattleState state;
    public Target choice;
    public FightMath.Option maneuver;
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

        if(state == BattleState.CALCULATING)
        {
            setCountDown(true);

            if(checkUnits())
            {
                if(goingUnit() != null)
                {
                    currentUnit = goingUnit();
                    state = stateIn(currentUnit);
                }
            }
            else
            {
                foreach(Unit unit in playerOrder)
                {
                    unit.countBack(battleSpeed);
                }
            }
        }     

        if(state == BattleState.PLAYERTURN)
        {
            if (choose == false)
            {
                ui.TogglePlayer(true);

                ui.setAbilities(currentUnit.GetComponent<Hero>());

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
                        case FightMath.Option.ATTACK:
                            StartCoroutine(playerAttack(selectOne));
                            break;
                        case FightMath.Option.LIFE:
                            StartCoroutine(playerHeal(selectOne));
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
                    setUnit(false, enemyPool[0], enemyPos2);
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
        setUnit(true, playerPool[1], heroPos2);
        setUnit(true, playerPool[2], heroPos3);
        setUnit(true, playerPool[3], heroPos4);
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

    // Gets the players
    public List<Unit> getPlayers()
    {
        return players;
    }

    // Gets the enemies
    public List<Unit> getEnemies()
    {
        return monsters;
    }

    // Gets all
    public List<Unit> getAll()
    {
        return playerOrder;
    }

    // This script starts the battle
    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(1f);
        ui.ToggleOverhead(false);
        determineTurnOrder();
        currentUnit = playerOrder[unitMoving];
        state = BattleState.CALCULATING;
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

    public void ChoiceMade(FightMath.Option opt)
    {
        maneuver = opt;
        select();
    }

    // Has an opponent take damage
    public void dealDamage(int damage, Unit target)
    {
        target.ColorDamage(Color.black);

        target.StartCoroutine(target.DamageDisplay(damage, 0.2f));

        target.TakeDamage(damage);
    }

    // Heals a unit
    public void heal(int strength, Unit target)
    {
        target.ColorDamage(Color.green);

        target.StartCoroutine(target.DamageDisplay(strength, 0.2f));

        target.Restore(strength);
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

    public IEnumerator playerHeal(Unit target)
    {
        heal(currentUnit.getStrength(), target);

        yield return new WaitForSeconds(0f);

        choose = false;

        nextTurn();
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
    
    public bool checkUnits()
    {
        foreach(Unit unit in playerOrder)
        {
            if(unit.getTime() <= 0)
            {
                unit.setCurrent(0);
                return true;
            }
        }
        return false;
    }

    public Unit goingUnit()
    {
        foreach (Unit unit in playerOrder)
        {
            if(unit.getTime() <= 0)
            {
                return unit;
            }
        }
        return null;
    }

    public void nextTurn()
    {
        currentUnit.resetTime();

        state = BattleState.CALCULATING;

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

    public void attackTalk(string words)
    {
        ui.ToggleOverhead(true);
        ui.SetBattleDescription(words);
    }

    public void disableTalk()
    {
        ui.ToggleOverhead(false);
    }

    //Physically removes a unit
    public void RemoveUnit(Unit toRemove)
    {
        if(toRemove.isPlayer)
        {
            indexPoint = players.IndexOf(toRemove);
            players.Remove(toRemove);
        }
        else
        {
            indexPoint = monsters.IndexOf(toRemove);
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

    public void setCountDown(bool active)
    {
        foreach(Unit unit in playerOrder)
        {
            if(unit.isDead == false)
            {
                unit.ToggleDelay(active);
            }
            else
            {
                unit.ToggleDelay(false);
            }
        }
    }

    //merges for merge sort
    public void MergeUnits(List<Unit> unts, int lowerB, int mid, int upperB)
    {

    }

    public void UnitSort(List<Unit> unts, int lowerB, int upperB)
    {

    }

    public void SwapUnits(Unit u1, Unit u2)
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
