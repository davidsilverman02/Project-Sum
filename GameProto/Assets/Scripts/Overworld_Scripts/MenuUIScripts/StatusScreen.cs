using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StatSave;

public class StatusScreen : MonoBehaviour
{
    public Image portrait;

    public TMP_Text charaName;

    public TMP_Text hpText;
    public TMP_Text ppText;

    public Slider hpBar;
    public Slider ppBar;

    public TMP_Text attackTxt;
    public TMP_Text magicTxt;
    public TMP_Text defenseTxt;
    public TMP_Text speedTxt;

    public void setCurrent(StatContainer.StatObject player)
    {
        hpText.text = player.currentHP.ToString() + "/" + player.maxHP.ToString();
        ppText.text = player.currentPP.ToString() + "/" + player.maxPP.ToString();

        hpBar.maxValue = player.maxHP;
        hpBar.value = player.currentHP;

        ppBar.maxValue = player.maxPP;
        ppBar.value = player.currentPP;

        attackTxt.text = player.attack.ToString();
        magicTxt.text = player.magic.ToString();
        defenseTxt.text = player.defense.ToString();
        speedTxt.text = player.speed.ToString();
    }
}
