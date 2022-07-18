using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StatSave;

public class UnitInMenu : MonoBehaviour
{
    public bool selected;

    public Image portrait;

    public TMP_Text charaName;

    public TMP_Text hpText;
    public TMP_Text ppText;

    public Slider hpBar;
    public Slider ppBar;

    public GameObject recticle;



    public void setSelf(StatContainer.StatObject player)
    {
        charaName.text = player.unitName;

        hpText.text = player.currentHP.ToString() + "/" + player.maxHP.ToString();
        ppText.text = player.currentPP.ToString() + "/" + player.maxPP.ToString();

        hpBar.maxValue = player.maxHP;
        hpBar.value = player.currentHP;

        ppBar.maxValue = player.maxPP;
        ppBar.value = player.currentPP;
    }
}
