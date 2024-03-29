using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, CALCULATING, PLAYERTURN, ENEMYTURN, WIN, LOSE }
public enum Target {SELF, ONE}

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

    public float decompRate = 0.05f;

    public GameManager manager;
    public BattleUIManager ui;

    public List<Transform> heroPos;

    public List<Transform> enemyPos;

    public List<Unit> players;
    public List<Unit> monsters;

    public BattleState state;
    public Target choice;
    public FightMath.Option maneuver;
    public Unit currentUnit;
    public Unit selectOne;
    public bool choose;
    public bool skillChoice;
    public bool onEnemy;
    public bool turnCalled;
    public bool hasEnded;
    public bool executed;
    public GameObject[] enemyPool = new GameObject[POOL_NUM];
    public GameObject[] playerPool = new GameObject[PLAYERS];

    public List<Unit> playerOrder;
    public List<Unit> selected;
    public List<Unit> uiOrder;

    public List<int> uiOrderCalc;

    public List<GameObject> skills;
    public GameObject skillItem;

    public GameObject bar;

    public TurnBar turnBar;

    int attackRank;

    public Skill playerSkill;

    public int positionNum = 10;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        ui = FindObjectOfType<BattleUIManager>();
        turnBar = FindObjectOfType<TurnBar>();
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
        //monsters.Count <= 0 || allDead(monsters)
        if (monsters.Count <= 0)
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
                    if(unit.CanFight())
                    {
                        unit.countBack(battleSpeed);
                    }
                }
            }
        }     

        if(state == BattleState.PLAYERTURN)
        {
            if (choose == false && skillChoice == false)
            {
                ui.TogglePlayer(true);

                ui.ToggleOptions(false);

                ui.setAbilities(currentUnit.GetComponent<Hero>());

                disableSelectors();
            }
            else if(choose == false && skillChoice == true)
            {
                ui.TogglePlayer(false);

                ui.ToggleOptions(true);

                disableSelectors();
            }
            else
            {
                ui.TogglePlayer(false);

                ui.ToggleOptions(false);

                switch(choice)
                {
                    case Target.SELF:
                        setSelect(currentUnit);
                        break;
                    case Target.ONE:
                        oneSelect();

                        if (onEnemy)
                        {
                            selectOne = monsters[selectedUnit];
                        }
                        else
                        {
                            selectOne = players[selectedUnit];
                        }
                        break;
                }
                
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    switch (maneuver)
                    {
                        case FightMath.Option.ATTACK:
                            attackRank = 0;
                            StartCoroutine(useSkill(currentUnit.attack, selectedUnit));
                            break;
                        case FightMath.Option.SKILL:
                            attackRank = 0;
                            StartCoroutine(useSkill(playerSkill, selectedUnit));
                            break;
                        case FightMath.Option.DEFEND:
                            attackRank = 0;
                            StartCoroutine(useSkill(currentUnit.defend, 0));
                            break;
                    }
                    
                }
            }
        }
        else
        {
            ui.TogglePlayer(false);
            ui.ToggleOptions(false);
            
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
                    setUnit(false, enemyPool[0], enemyPos[0]);
                    setUnit(false, enemyPool[0], enemyPos[1]);
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
        for(int i = 0; i < manager.party.Count; i++)
        {
            setUnit(true, playerPool[manager.party[i]], heroPos[i]);
            players[i].GetComponent<Hero>().setStats(manager.players[manager.party[i]]);
        }
    }

    void exitBattle()
    {
        for(int i = 0; i < manager.party.Count; i++)
        {
            manager.players[i].set(players[i].GetComponent<Hero>().getStats());
        }
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

        /*
        foreach (Unit unit in players)
        {
            playerOrder.Add(unit);
        }
        */

        setPlayerUIOrder();
    }

    // Has the player select which unit is being targetted in battle
    void select(Skill skill)
    {
        choice = Target.SELF;

        for(int i = 0; i < skill.targets.Length; i++)
        {
            if(skill.targets[i].target == Target.ONE)
            {
                choice = Target.ONE;
            }
        }

        choose = true;
    }

    public void selectPower(Skill skill)
    {
        select(skill);

        playerSkill = skill;
    }

    // Has Enemies select
    public void enemySelect(Skill skill)
    {
        choice = Target.SELF;

        for(int i = 0; i<skill.targets.Length; i++)
        {
            if(skill.targets[i].target == Target.ONE)
            {
                choice = Target.ONE;
            }
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

    void setSelect(Unit unit)
    {
        unit.ToggleSelected(true);
    }

    public void ChoiceMade(FightMath.Option opt)
    {
        maneuver = opt;
        if(maneuver == FightMath.Option.SKILL)
        {
            ActivateSkills(false);
        }
        else if(maneuver == FightMath.Option.ITEM)
        {

        }
        else if(maneuver == FightMath.Option.ATTACK)
        {
            select(currentUnit.attack);
        }
        else if(maneuver == FightMath.Option.DEFEND)
        {
            select(currentUnit.defend);
        }
    }

    public void ActivateSkills(bool isItems)
    {
        foreach(GameObject obj in skills)
        {
            GameObject.Destroy(obj);
        }

        skillChoice = true;
        skills.Clear();

        if(isItems)
        {
            
        }
        else
        {
            placeSkills();
        }
    }

    public void placeSkills()
    {
        for (int i = 0; i < currentUnit.GetComponent<Hero>().powers.Count; i++)
        {
            GameObject skill = Instantiate(skillItem, bar.transform.position, bar.transform.rotation) as GameObject;
            skill.GetComponent<RectTransform>().SetParent(bar.transform, false);
            skill.GetComponent<SkillUI>().setSkill(currentUnit.GetComponent<Hero>().powers[i]);
            skills.Add(skill);
        }
    }

    // Has an opponent take damage
    public void dealDamage(int damage, Unit target)
    {
        target.StartCoroutine(target.DamageDisplay(damage, 0.2f));

        target.TakeDamage(damage);
    }

    //public IEnumerator damageDisplay()

    // Heals a unit
    public void heal(int strength, Unit target)
    {
        target.ColorDamage(Color.green);

        target.StartCoroutine(target.DamageDisplay(strength, 0.2f));

        target.Restore(strength);
    }

    public IEnumerator useSkill(Skill skill, int target)
    {
        bool enemyDied = false;

        interpretSkill(skill, goingUnit(), target);

        yield return new WaitForSeconds(goingUnit().attack.duration);

        ui.ToggleOverhead(false);

        foreach (Unit targe in monsters)
        {
            if (targe.Dead())
            {
                StartCoroutine(killEnemy(targe));
                enemyDied = true;
            }
        }

        if (enemyDied)
        {
            yield return new WaitForSeconds(1f);
        }

        nextTurn();
    }
    
    public IEnumerator killEnemy(Unit target)
    {
        for (float i = 1f; i > 0.0f; i -= decompRate)
        {
            yield return new WaitForSeconds(decompRate);
        }

        RemoveUnit(target);

        selectOne = null;

        determineTurnOrder();
    }
    
    public bool checkUnits()
    {
        foreach(Unit unit in playerOrder)
        {
            if(unit.getTime() <= 0 && unit.CanFight())
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
            if(unit.getTime() <= 0 && unit.CanFight())
            {
                return unit;
            }
        }
        return null;
    }

    public void nextTurn()
    {
        currentUnit.resetTime();

        setPlayerUIOrder();

        state = BattleState.CALCULATING;

        turnCalled = false;

        choose = false;

        skillChoice = false;
    }

    public void setRank(int strength)
    {
        attackRank = strength;
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

    void setUpOrder()
    {
        uiOrderCalc.Clear();

        for(int i = 0; i < playerOrder.Count; i++)
        {
            uiOrderCalc.Add(playerOrder[i].getTime());
        }
    }

    bool findZero()
    {
        foreach(int calc in uiOrderCalc)
        {
            if(calc <= 0)
            {
                return true;
            }
        }
        return false;
    }

    int goingTime()
    {
        for (int i = 0; i < uiOrderCalc.Count; i++)
        {
            if(uiOrderCalc[i] <= 0)
            {
                return i;
            }
        }
        return -1;
    }

    public List<Unit> getPlayer()
    {
        return uiOrder;
    }

    public void setPlayerUIOrder()
    {
        setUpOrder();

        uiOrder.Clear();

        while(uiOrder.Count < positionNum)
        {
            if(findZero())
            {
                if(goingTime() >= 0)
                {
                    uiOrder.Add(playerOrder[goingTime()]);
                    uiOrderCalc[goingTime()] = playerOrder[goingTime()].getDelay();
                }
            }
            else
            {
                for(int i = 0; i < uiOrderCalc.Count; i++)
                {
                    uiOrderCalc[i]--;
                }
            }
        }

        turnBar.SetTurns(uiOrder, players);

    }

    //Physically removes a unit
    public void RemoveUnit(Unit toRemove)
    {
        if(toRemove.isPlayer)
        {
            indexPoint = players.IndexOf(toRemove);
            players.Remove(toRemove);
            if (selectedUnit >= players.Count)
            {
                selectedUnit--;
            }
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

    public void interpretSkill(Skill inta, Unit user, int target)
    {
        ui.SetBattleDescription(inta.name);
        ui.ToggleOverhead(true);

        attackRank = inta.priority;

        user.setForTurn(attackRank);

        for (int i = 0; i < inta.targets.Length; i++)
        {
            Unit affected = null;
            int calculated;

            switch (choice)
            {
                case Target.SELF:
                    affected = user;
                    break;
                case Target.ONE:
                    if (onEnemy)
                    {
                        affected = monsters[target];
                    }
                    else
                    {
                        affected = players[target];
                    }
                    break;
            }

            if (inta.targets[i].GetEffect().damage != 0)
            {
                if((choice == Target.ONE || choice == Target.SELF))
                {
                    FightMath.CalculateDamage(inta, user, affected, i, inta.targets[i].effects.drain);
                }
                else
                {

                }
            }

            if(inta.targets[i].GetEffect().healing != 0)
            {
                if ((choice == Target.ONE || choice == Target.SELF))
                {
                    FightMath.CalculateHealing(inta, user, affected, i);
                }
                else
                {

                }
            }

            if(inta.targets[i].GetEffect().appliesSlow)
            {

            }

            if(inta.targets[i].GetEffect().appliesDefend)
            {
                affected.buffDefense(3, affected.getDelay());
            }
        }

        user.DrainPower(inta.cost);
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

    public void setTarget(bool enemies)
    {
        onEnemy = enemies;
    }

    public bool getTarget()
    {
        return onEnemy;
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

        exitBattle();

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
