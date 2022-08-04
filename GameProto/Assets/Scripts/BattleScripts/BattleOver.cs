using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleOver : MonoBehaviour
{
    public BattleManager man;

    public TMP_Text description;

    public TMP_Text expIn;
    public GameObject placer;

    public List<LevelBar> bars;
    public GameObject bar;

    // Start is called before the first frame update
    void Awake()
    {
        man = FindObjectOfType<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setEXP(int exp)
    {
        expIn.text = "EXP: " + exp.ToString();
    }

    public void playerDisplay()
    {
        for(int i = 0; i < man.manager.party.Count; i++)
        {
            GameObject pler = Instantiate(bar, placer.transform) as GameObject;
            bars.Add(pler.GetComponent<LevelBar>());
            bars[i].setBar(man.manager.players[man.manager.party[i]]);
            bars[i].updateBar(man.manager.players[man.manager.party[i]]);
        }
    }

    public void updateBars()
    {
        for (int i = 0; i < man.manager.party.Count; i++)
        {
            bars[i].setBar(man.manager.players[man.manager.party[i]]);
            bars[i].updateBar(man.manager.players[man.manager.party[i]]);
        }
    }

    public List<LevelBar> getBars()
    {
        return bars;
    }
}
