using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public NPCDisplay display;

    public GameManager man;

    public bool isTalking;

    public List<string> dialogue;

    // Start is called before the first frame update
    void Start()
    {
        man = FindObjectOfType<GameManager>();

        man.addNPC(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        
    }

    public Vector3 getPos()
    {
        return gameObject.transform.position;
    }

    public void resetDisplay()
    {
        display.Reset();
    }

    public void canChat(bool can)
    {
        display.setCanChat(can);
    }

    public void stopChat()
    {
        man.isTalk = false;
    }
}
