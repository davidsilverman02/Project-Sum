using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using StatSave;

public class MenuManager : MonoBehaviour
{
    public enum MenuState { MAIN = 0, PARTY = 1, INVENTORY = 2, EQUIP = 3, STATUS = 4, GRID = 5, CONFIG = 6, HELP = 7 }

    public GameManager manager;

    public GameObject all;
    public GameObject playerOrder;
    public GameObject charaProf;
    public GameObject buttonBar;
    public GameObject scrollBar;

    public MenuState stat;

    public GameObject chara;
    public List<UnitInMenu> charas;

    public int selected;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        setUp();
        setMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && (manager.party.Count >= 1 || manager.party.Count <= 4))
        {
            setMode(0);
        }

        switch(stat)
        {
            case MenuState.PARTY:
                onSwitch();

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if(manager.party.Contains(selected))
                    {
                        manager.party.Remove(selected);
                    }
                    else
                    {
                        manager.party.Add(selected);
                    }
                }
                break;
        }
        cycle();
    }

    public void cycle()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(selected == 0)
            {
                selected = getPlayers().Count - 1;
            }
            else
            {
                selected--;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (selected == getPlayers().Count - 1)
            {
                selected = 0;
            }
            else
            {
                selected++;
            }
        }
    }

    public void onSwitch()
    {
        for(int i = 0; i < charas.Count; i++)
        {
            if(inParty(i))
            {
                charas[i].displayIn(true);
            }
            else
            {
                charas[i].displayIn(false);
            }

            if(i == selected)
            {
                charas[i].setChosen(true);
            }
            else
            {
                charas[i].setChosen(false);
            }
        }
    }

    public void setMenu(bool set)
    {
        Time.timeScale = System.Convert.ToSingle(!set);
        
        all.SetActive(set);
        if(set == false)
        {
            setMode(0);
        }
    }

    public void setMode(int state)
    {
        stat = (MenuState)state;

        switch(stat)
        {
            case MenuState.MAIN:
                turnOffSelect();
                playerOrder.SetActive(true);
                enablePlayers(true);
                charaProf.SetActive(false);
                buttonBar.SetActive(true);
                scrollBar.SetActive(false);
                break;
            case MenuState.PARTY:
                playerOrder.SetActive(true);
                enablePlayers(true);
                charaProf.SetActive(false);
                buttonBar.SetActive(false);
                scrollBar.SetActive(false);
                break;
            case MenuState.STATUS:
                playerOrder.SetActive(false);
                enablePlayers(false);
                charaProf.SetActive(true);
                buttonBar.SetActive(true);
                scrollBar.SetActive(true);
                break;
        }
    }

    public void enablePlayers(bool enable)
    {
        for(int i = 0; i < getPlayers().Count; i++)
        {
            charas[i].gameObject.SetActive(enable);
        }
    }

    public void turnOffSelect()
    {
        foreach(UnitInMenu cha in charas)
        {
            cha.clearOut(false);
        }
    }


    public List<StatContainer.StatObject> getPlayers()
    {
        return manager.players;
    }

    public StatContainer.StatObject getCurrent()
    {
        return getPlayers()[selected];
    }

    public void generateChara()
    {
        for (int i = 0; i < getPlayers().Count; i++)
        {
            GameObject cha = Instantiate(chara.gameObject, playerOrder.transform);
            cha.GetComponent<UnitInMenu>().setSelf(getPlayers()[i]);
            charas.Add(cha.GetComponent<UnitInMenu>());
        }
    }

    public void setUp()
    {
        selected = 0;
        charas.Clear();
        generateChara();
        turnOffSelect();
        setMode(0);
    }

    public bool inParty(int set)
    {
        for(int i = 0; i < manager.party.Count; i++)
        {
            if(set == manager.party[i])
            {
                return true;
            }
        }
        return false;
    }
}
