using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isPlayer;
    public bool hasTurn;

    public string unitName;
    public int unitLevel;

    // Individual Stats for a unit
    public int strength;
    public int defense;


    public int currentHP;
    public int maxHP;
    public int currentPP;
    public int maxPP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }
}
