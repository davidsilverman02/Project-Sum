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
        const int HPRATE = 2;
        const int PPRATE = 1;

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

            public void levelAdd(int level, StatObject addTo)
            {
                foreach(LevelElem elem in mileStones)
                {
                    if(elem.level == level)
                    {
                        foreach(Skill skill in elem.add)
                        {
                            addTo.skills.Add(skill);
                        }
                    }
                }
            }

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

            [SerializeField]
            private LoadStat vitality;

            [SerializeField]
            private LoadStat soul;

            public int currentHP;
            public int maxHP;
            public int currentPP;
            public int maxPP;

            public Weakness weakness;

            public Skill strike;
            public Skill defend;

            public List<Skill> skills;

            public GameObject model;

            public LevelSkillTree tree;

            public StatObject()
            {

            }

            public void set(StatObject sts, bool isUnleveled)
            {
                unitName = sts.unitName; 

                if(isUnleveled)
                {
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
            }

            public void addEXP(int added)
            {
                exPoints -= added;

                while(exPoints <= 0)
                {
                    level++;
                    calcNewStats(level);
                    exPoints += newLevel();
                }
            }

            public int newLevel()
            {
                return Mathf.RoundToInt(LEVELRATE * Mathf.Sqrt(level));
            }

            public void calcHP()
            {
                maxHP = vitality.getEffective() * HPRATE;
            }

            public void calcPP()
            {
                maxPP = soul.getEffective() * PPRATE;
            }

            public void calcNewStats(int newLevel)
            {
                int hpDifference = maxHP - currentHP;
                int ppDifference = maxPP - currentPP;

                attack.setSubtotal(level, false);
                magic.setSubtotal(level, false);
                defense.setSubtotal(level, false);
                wisdom.setSubtotal(level, false);
                agility.setSubtotal(level, false);
                speed.setSubtotal(level, false);

                vitality.setSubtotal(level, false);
                soul.setSubtotal(level, false);

                calcHP();
                calcPP();

                currentHP = maxHP - hpDifference;
                currentPP = maxPP - ppDifference;

                tree.levelAdd(newLevel, this);
            }
        }
    }
}

