using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public BattleUIManager manager;

    public GameObject selector;

    public List<Transform> slector;
    public List<Text> names;

    public void ActivateUnits()
    {
        for(int i = 0; i < manager.bManager.getPlayerCount(); i++)
        {
            names[i].enabled = true;
            names[i].text = manager.bManager.players[i].getName();
        }
    }

    public void ClearUnits()
    {
        for(int i = 0; i > slector.Count; i++)
        {
            names[i].enabled = false;
        }
    }

    public void selectActive(bool act)
    {
        selector.SetActive(act);
    }

    public void selectUsing(int act)
    {
        selector.transform.position = slector[act].position;
    }

    public void selectEnemy(int going)
    {
        selector.transform.position = slector[going].position;
    }
}
