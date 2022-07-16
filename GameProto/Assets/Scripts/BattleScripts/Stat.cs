using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    int power;
    public List<Vector2Int> buffs;


    public Stat()
    {

    }

    public Stat(int strength)
    {
        power = strength;
    }

    public int getRawPower()
    {
        return power;
    }

    public int get()
    {
        return Mathf.RoundToInt((float)power * buffMulti());
    }

    public void setPower(int strength)
    {
        power = strength;
    }

    public void countBack(int spd)
    {   
        /*
        for(int i = 0; i < buffs.Count; i++)
        {
            buffs[i] = new Vector2Int(buffs[i].x, buffs[i].y - spd);
        }
        */

        //
        for (int i = buffs.Count - 1; i > -1; i--)
        {
            buffs[i] = new Vector2Int(buffs[i].x, buffs[i].y - spd);
            if(buffs[i].y <= 0)
            {
                buffs.RemoveAt(i);
            }
        }
        //

        /*
        foreach(Vector2Int vecs in buffs)
        {
            if(vecs.y <= 0)
            {
                buffs.Remove(vecs);
            }
        }
        */
    }

    public void clearBuffs()
    {
        buffs.Clear();
    }

    public void addBuff(Vector2Int buff)
    {
        buffs.Add(buff);
    }

    float buffMulti()
    {
        float multi = 0.0f;

        foreach(Vector2Int vec in buffs)
        {
            multi += vec.x;
        }

        multi = Mathf.Clamp((float)multi, -6.0f, 6.0f);

        return 1.0f + (multi * 0.125f);
    }
}
