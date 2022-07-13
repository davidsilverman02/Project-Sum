using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverworldUIManager : MonoBehaviour
{
    public BattleManager man;

    public GameObject chatBar;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setChatBar(bool isOn)
    {
        chatBar.SetActive(isOn);
    }

    public void setText(string dia)
    {
        text.text = dia;
    }

    public void Reset()
    {
        setChatBar(false);
    }
}
