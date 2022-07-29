using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WeaknessCalculator;

namespace StatSave
{
    public class StatContainer : MonoBehaviour
    {
        const float LEVELRATE = 100.0f;

        [Serializable]
        public class LoadStat
        {
            public float rate;
            public int subtotal;
            public int bonus;

            public void setSubtotal(int level, bool isStoring)
            {
                if(isStoring)
                {
                    subtotal = level;
                }
                else
                {
                    subtotal = Mathf.RoundToInt(rate * (float)level * 5.0f);
                }
            }

            public int getSubtotal()
            {
                return subtotal;
            }

            public void setBonus(int bons)
            {
                bonus = bons;
            }

            public void addBonus(int bons)
            {
                bonus += bons;
            }

            public int getBonus()
            {
                return bonus;
            }

            public int getEffective()
            {
                return subtotal + bonus;
            }

        } 

        [Serializable]
        public class LevelElem
        {
            public int level;
            public List<Skill> add;
        }

        [Serializable]
        public class LevelSkillTree
        {
            public List<LevelElem> mileStones;
        }

        [Serializable]
        public class StatObject
        {
            public string name;

            public string unitName;

            public int level;

            public int exPoints;

            public LoadStat attack;
            public LoadStat magic;
            public LoadStat defense;
            public LoadStat wisdom;
            public LoadStat agility;
            public LoadStat speed;

            public int currentHP;
            public int maxHP;
            public int currentPP;
            public int maxPP;

            public Weakness weakness;

            public Skill strike;
            public Skill defend;

            public List<Skill> skills;

            public GameObject model;

            public StatObject()
            {

            }

            public void set(StatObject sts)
            {
                unitName = sts.unitName; 

                attack = sts.attack;
                magic = sts.magic;
                defense = sts.defense;
                wisdom = sts.wisdom;
                agility = sts.agility;
                speed = sts.speed;

                currentHP = sts.currentHP;
                maxHP = sts.maxHP;
                currentPP = sts.currentPP;
                maxPP = sts.maxPP;
            }

            public int newLevel()
            {
                return Mathf.RoundToInt(LEVELRATE * Mathf.Sqrt(level));
            }
        }
    }
}

