using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public BattleManager bManager;

    public GameObject all;

    public GameObject descBox;
    public GameObject optionsBox;
    public TMP_Text battleDesc;

    // Start is called before the first frame update
    void Start()
    {
        bManager = FindObjectOfType<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Sets the description of the battle
    public void SetBattleDescription(string txt)
    {
        battleDesc.text = txt;
    }

    // Turns the description box on and off
    public void ToggleOverhead(bool tog)
    {
        descBox.SetActive(tog);
    }

    // Toggles player info
    public void TogglePlayer(bool tog)
    {
        optionsBox.SetActive(tog);
    }
}
