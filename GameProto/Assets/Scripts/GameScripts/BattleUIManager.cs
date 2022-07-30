using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public BattleManager bManager;
    public SkillBar optionsBox;
    public SkillItemBar options;

    public GameObject all;

    public GameObject descBox;
    public TMP_Text battleDesc;

    public GameObject winScreen;

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
        optionsBox.gameObject.SetActive(tog);
    }

    public void ToggleOptions(bool tog)
    {
        options.gameObject.SetActive(tog);
    }

    public void setAbilities(Hero assign)
    {
        //optionsBox.SetSkills(assign);
    }
}
