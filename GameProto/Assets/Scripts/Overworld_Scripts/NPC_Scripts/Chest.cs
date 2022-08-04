using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameManager man;
    public int id;

    public Item treasure;
    public bool isOpened;

    public void Start()
    {
        man = FindObjectOfType<GameManager>();

        man.addChest(this);

        isOpened = man.opened[id];
    }

    public void Open()
    {
        isOpened = true;
        man.opened[id] = true;
        man.addItem(treasure);
        man.setBar(true);
        speak();
    }

    public void Exit()
    {
        man.setBar(false);
        man.setPlay(false);
        man.isTalk = false;
    }

    public Vector3 getPos()
    {
        return gameObject.transform.position;
    }

    public virtual void speak()
    {
        man.setUIChat("You found one " + treasure.name);
    }
}
