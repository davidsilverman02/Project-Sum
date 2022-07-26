using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatSave;
using System;

[Serializable]
public class SaveData 
{
    public int level;
    public float playerX;
    public float playerY;
    public float playerZ;

    public List<int> group;
    public List<StatContainer.StatObject> stObjs;

    public SaveData()
    {

    }

    public SaveData(GameManager manager)
    {
        level = manager.level;
        playerX = manager.spawnX;
        playerY = manager.spawnY;
        playerZ = manager.spawnZ;

        group = manager.party;
        stObjs = manager.players;
    }
}
