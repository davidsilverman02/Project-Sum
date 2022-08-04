using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public BattleManager manager;

    public Consumable eat;

    public Image img;

    public TMP_Text itemName;

    public TMP_Text itemCount;

    
    public void Start()
    {
        manager = FindObjectOfType<BattleManager>();
    }

    public void setItem(Consumable power)
    {
        eat = power;
        itemName.text = power.name;
        itemCount.text = manager.manager.inventory.Get(power).invenSize.ToString();
    }

    public void useItem()
    {
        
    }
}
