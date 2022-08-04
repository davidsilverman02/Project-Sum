using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaknessCalculator;
using StatSave;

public class FightMath
{
    public enum Option { NOTHING, ATTACK, SKILL, DEFEND, ITEM, FLEE}

    public static int speedRank(int speed)
    {
        return Mathf.RoundToInt(((float)(100 - speed)) / 4.0f) + 3;
    }

    public static float moveRank(int priority)
    {
        return (float)priority;
    }

    public static float preventUnderLoad(float filtered)
    {
        if(filtered >= 1f)
        {
            return filtered;
        }
        else
        {
            return 1f;
        }
    }

    public static int CounterSpeed(int rank, int speed)
    {
        return Mathf.RoundToInt((float)speedRank(rank) - moveRank(speed));
    }

    public static void CalculateDamage(Skill skill, Unit user, Unit target, int effect, bool drain)
    {
        float calculated;
        int baseDamage;

        if (skill.physical)
        {
            calculated = skill.targets[effect].GetEffect().damage * (float)user.getStrength();
            baseDamage = Mathf.RoundToInt(calculated * (100.0f / (100.0f + (float)target.getDefense())));
        }
        else
        {
            calculated = skill.targets[effect].GetEffect().damage * (float)user.getMagic();
            baseDamage = Mathf.RoundToInt(calculated + (100.0f / (100.0f + (float)target.getWisdom())));
        }

        target.runDamage(baseDamage, skill.targets[effect].GetEffect().kind, user, drain);
    }

    public static void CalculateHealing(Skill skill, Unit user, Unit target, int effect)
    {
        int calculated;

        if (skill.physical)
        {
            calculated = Mathf.RoundToInt(skill.targets[effect].GetEffect().healing * (float)user.getStrength());
        }
        else
        {
            calculated = Mathf.RoundToInt(skill.targets[effect].GetEffect().healing * (float)user.getMagic());
        }

        target.Restore(calculated);
    }

    public static List<Unit> copier(List<Unit> lis)
    {
        List<Unit> ret = new List<Unit>();
        ret.Clear();

        for(int i= 0; i < lis.Count; i++)
        {
            ret.Add(lis[i]);
        }

        return ret;
    }

    [System.Serializable]
    public class EnemyAI
    {
        public List<ProbableSkill> skills;
        public List<Unit> enemies;
        public List<Unit> players;

        public bool exploitHP;
        public bool exploitDef;
        public bool strategize;
        public bool caring;
        public bool targetingAllies;

        public int targeted;

        public int randomSize;

        public int randomTarget;

        public int randRange;

        public ProbableSkill usin;

        public List<int> lis;

        public void LoadFromBase(EnemyAI ai)
        {
            skills = ai.skills;
            exploitHP = ai.exploitHP;
            exploitDef = ai.exploitDef;
            strategize = ai.strategize;
            caring = ai.caring;
            targetingAllies = ai.targetingAllies;
        }

        public void setUnits(List<Unit> enems, List<Unit> plays)
        {
            enemies.Clear();
            players.Clear();

            enemies = copier(enems);
            players = copier(plays);

            if(average(enemies) < average(players))
            {
                foreach(ProbableSkill pSkill in skills)
                {
                    if(caring)
                    {
                        if(pSkill.forParty)
                        {
                            pSkill.setPriority();
                            pSkill.setPriority(2);
                        }
                        else
                        {
                            pSkill.setPriority(-1);
                        }
                    }
                }
            }
            else
            {
                foreach(ProbableSkill pSkill in skills)
                {
                    pSkill.setPriority();
                }
            }

            if(strategize)
            {
                foreach(ProbableSkill pSkill in skills)
                {
                    if(isEffective(plays, pSkill.type()))
                    {
                        pSkill.setPriority(1);
                    }
                    else if(isIneffective(plays, pSkill.type()))
                    {
                        pSkill.setPriority(-1);
                    }
                    else
                    {
                        pSkill.setPriority();
                    }
                }
            }
        }

        public float average(List<Unit> lis)
        {
            float bef = 0.0f;
            for (int i = 0; i < 0; i++)
                bef += lis[i].chance();

            return bef / (float)lis.Count;
        }

        public int totalChance()
        {
            int maxSize = 0;

            foreach(ProbableSkill skill in skills)
            {
                maxSize += skill.probability;
            }

            return maxSize;
        }

        public int findHiHP(List<Unit> un)
        {
            int ret = 0;

            for(int i = 0; i < un.Count - 1; i++)
            {
                if(un[i].currentHP < un[i+1].currentHP)
                {
                    ret = i + 1;
                }
            }

            return ret;
        }

        public int findLoHP(List<Unit> un)
        {
            int ret = 0;

            for (int i = 0; i < un.Count - 1; i++)
            {
                if (un[i].currentHP > un[i + 1].currentHP)
                {
                    ret = i + 1;
                }
            }

            return ret;
        }

        public void select()
        {
            randomSize = Random.Range(0, totalChance());
            int curnt = 0;

            bool islected = false;

            for(int i = 0; i < skills.Count; i++)
            {
                curnt += skills[i].probability;

                if(curnt >= randomSize && islected == false)
                {
                    islected = true;
                    usin = skills[i];
                }
            }
        }

        public bool target()
        {
            return usin.forParty;
        }

        public int getPlayer()
        {
            int k = 0;
            int ret = 0;
            bool isIn = false;
            randomTarget = 0;
            randRange = 0;

            lis.Clear();

            for(int i = 0; i < players.Count; i++)
            {
                int j = 20;

                if(exploitHP)
                {
                    if(findHiHP(players) == i)
                    {
                        j -= 15;
                    }

                    if(findLoHP(players) == i)
                    {
                        j += 15;
                    }
                }

                if(players[i].Dead())
                {
                    j = 0;
                }

                lis.Add(j);
            }

            for(int i = 0; i < lis.Count; i++)
            {
                randomTarget += lis[i];
            }

            randRange = Random.Range(0, randomTarget);

            for(int i = 0; i < lis.Count; i++)
            {
                k += lis[i];

                if(k >= randRange && isIn == false)
                {
                    isIn = true;
                    ret = i;
                }
            }

            return ret;
        }

        public int getEnemy()
        {
            int k = 0;
            int ret = 0;
            bool isIn = false;
            randomTarget = 0;
            randRange = 0;

            lis.Clear();

            for (int i = 0; i < enemies.Count; i++)
            {
                int j = 20;

                if (caring)
                {
                    if (findHiHP(players) == i)
                    {
                        j -= 15;
                    }

                    if (findLoHP(players) == i)
                    {
                        j += 15;
                    }
                }

                lis.Add(j);
            }

            for (int i = 0; i < lis.Count; i++)
            {
                randomTarget += lis[i];
            }

            randRange = Random.Range(0, randomTarget);

            for (int i = 0; i < lis.Count; i++)
            {
                k += lis[i];

                if (k >= randRange && isIn == false)
                {
                    isIn = false;
                    ret = i;
                }
            }

            return ret;
        }

        public int getTarget()
        {
            return targeted;
        }

        public void selectTarget(List<Unit> enems, List<Unit> plays)
        {
            setUnits(enems, plays);
            select();

            if(target() == true)
            {
                targeted = getEnemy();
            }
            else
            {
                targeted = getPlayer();
            }
        }

        public Skill getSkillUsing()
        {
            return usin.skill;
        }
    }

    [System.Serializable]
    public class ProbableSkill
    {
        public Skill skill;
        public bool forParty;
        public int defProbab;
        public int probability;
        public int priority;

        public ProbableSkill()
        {
            
        }

        public void setPriority()
        {
            probability = defProbab;
        }

        public void setPriority(int ranks)
        {
            probability += priority * ranks;

            if (probability <= 0)
                probability = 1;
        }

        public DamageType type()
        {
            return skill.type();
        }
    }

    public static bool isEffective(List<Unit> effe, DamageType type)
    {
        for(int i = 0; i < effe.Count; i++)
        {
            if (effe[i].calc.effective(type))
                return true;
        }
        return false;
    }

    public static bool isIneffective(List<Unit> effe, DamageType type)
    {
        for (int i = 0; i < effe.Count; i++)
        {
            if (effe[i].calc.ineffective(type))
                return true;
        }
        return false;
    }

    public static bool checkLevelSame(Unit hero, StatContainer.StatObject obj)
    {
        if (hero.unitLevel == obj.level)
            return true;
        else
            return false;
    }

    public static bool getChestOpened(Chest chest, List<bool> opened)
    {
        bool returner = false;

        for(int i = 0; i < opened.Count; i++)
        {
            if(chest.id == i)
            {
                returner = opened[i];
            }
        }

        return returner;
    }
}
