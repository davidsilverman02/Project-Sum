using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaknessCalculator;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public int hpRestore;
    public Target target;
}
