using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitUI : MonoBehaviour
{
    public GameObject selected;
    public TMP_Text text;
    public TMP_Text drain;
    public TMP_Text delayNum;
    public Slider hpBar;
    public Slider ppBar;
    

    public void Start()
    {
        Reset();
    }

    public void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
    }

    public void SetTarget(bool active)
    {
        selected.SetActive(active);
    }

    public void SetDamage(bool active)
    {
        text.gameObject.SetActive(active);
    }

    public void SetDrain(bool active)
    {
        drain.gameObject.SetActive(active);
    }

    public void DamageLevel(int level)
    {
        text.text = level.ToString();
    }

    public void DrainLevel(int level)
    {
        drain.text = level.ToString();
    }

    public void DamageColor(Color colo)
    {
        text.color = colo;
    }

    public void DrainColor(Color colo)
    {
        drain.color = colo;
    }

    public void SetHPBar(int set)
    {
        hpBar.value = set;
    }

    public void setMaxHP(int set)
    {
        hpBar.maxValue = set;
    }

    public void setPPBar(int set)
    {
        ppBar.value = set;
    }

    public void setMaxPP(int set)
    {
        ppBar.maxValue = set;
    }

    public void SetDelay(bool active)
    {
        delayNum.gameObject.SetActive(active);
    }

    public void DelayLevel(int level)
    {
        delayNum.text = level.ToString();
    }

    public void Reset()
    {
        SetTarget(false);
        SetDamage(false);
        SetDrain(false);
        SetDelay(false);
    }
}
