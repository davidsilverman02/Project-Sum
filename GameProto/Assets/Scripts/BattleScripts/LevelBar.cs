using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StatSave;
using TMPro;

public class LevelBar : MonoBehaviour
{
    public GameManager game;
    public BattleManager man;

    public TMP_Text charaName;
    public TMP_Text charLevel;
    public TMP_Text charEXP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBar(StatContainer.StatObject container)
    {
        charaName.text = container.name;
    }

    public void updateBar(StatContainer.StatObject container)
    {
        charLevel.text = "Lvl: " + container.level;
        charEXP.text = "NxtLvl: " + container.exPoints;
    }
}
