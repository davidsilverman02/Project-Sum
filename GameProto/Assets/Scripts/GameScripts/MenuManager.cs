using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuManager : MonoBehaviour
{
    public GameObject all;

    public List<UnitInMenu> charas;
    public GameObject List;

    // Start is called before the first frame update
    void Start()
    {
        setMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMenu(bool set)
    {
        Time.timeScale = System.Convert.ToSingle(!set);
        all.SetActive(set);
    }
}
