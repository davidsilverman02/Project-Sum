using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUI : MonoBehaviour
{
    public BattleManager manager;

    public Skill skill;

    public Image img;

    public TMP_Text skillName;

    public TMP_Text ppCount;

    public void Start()
    {
        manager = FindObjectOfType<BattleManager>();
    }

    public void setSkill(Skill power)
    {
        skill = power;
        skillName.text = power.name;
        ppCount.text = power.cost.ToString();
    }

    public void useSkill()
    {
        if(manager.currentUnit.currentPP >= skill.cost)
        {
            manager.selectPower(skill);
        }
    }
}
