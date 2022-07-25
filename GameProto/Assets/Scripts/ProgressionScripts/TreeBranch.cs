using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatSave;

[System.Serializable]
public class TreeBranch : MonoBehaviour
{
    public bool active;

    public int cost;

    public TreeBranch up;
    public TreeBranch left;
    public TreeBranch down;
    public TreeBranch right;

    public UnlockedSkills unlock;
    
    public void activate(StatContainer.StatObject stobj)
    {
        if(active == false)
        {
            unlock.upgradeUnit(stobj);
        }
    }
}
