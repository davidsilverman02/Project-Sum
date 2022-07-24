using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBarUnit : MonoBehaviour
{

    public Unit representing;

    public Image border;

    public Sprite allyBorder;
    public Sprite enemyBorder;

    public void Setup (bool ally, Unit unit)
    {
        representing = unit;
        if (ally) border.sprite = allyBorder;
        else border.sprite = enemyBorder;
    }
}
