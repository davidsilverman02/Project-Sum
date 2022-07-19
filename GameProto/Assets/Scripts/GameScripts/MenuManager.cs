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
        if(Input.GetKeyDown(KeyCode.Z))
        {
            setMode(0);
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
            case 0:
                playerOrder.SetActive(true);
                enablePlayers(true);
                charaProf.SetActive(false);
                buttonBar.SetActive(true);
                scrollBar.SetActive(false);
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

    public List<StatContainer.StatObject> getPlayers()
    {
        return manager.players;
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
        charas.Clear();
        generateChara();
        setMode(0);
    }
}
