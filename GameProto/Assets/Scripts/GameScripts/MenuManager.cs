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

    public MenuState stat;

    public List<UnitInMenu> charas;
    public GameObject List;

    public int selected;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        setMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public List<StatContainer.StatObject> getPlayers()
    {
        return manager.players;
    }
}
