using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace StatSave
{
    public class StatContainer : MonoBehaviour
    {
        [Serializable]
        public class StatObject
        {
            public string name;

            public string unitName;

            public int attack;
            public int magic;
            public int defense;
            public int speed;

            public int currentHP;
            public int maxHP;
            public int currentPP;
            public int maxPP;

            //public List<Skill> skills;

            public StatObject()
            {

            }

            public void set(StatObject sts)
            {
                unitName = sts.unitName; 

                attack = sts.attack;
                magic = sts.magic;
                defense = sts.defense;
                speed = sts.speed;

                currentHP = sts.currentHP;
                maxHP = sts.maxHP;
                currentPP = sts.currentPP;
                maxPP = sts.maxPP;
            }
        }
    }
}

