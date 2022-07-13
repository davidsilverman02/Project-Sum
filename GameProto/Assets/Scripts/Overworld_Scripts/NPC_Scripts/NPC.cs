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

    int currentLine;

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
        display.setCanChat(false);
        man.setBar(true);
        speak();
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

    public virtual void speak()
    {
        man.setUIChat(dialogue[currentLine]);
    }

    public virtual void Continue()
    {
        if(dialogue.Count - 1 <= currentLine)
        {
            Conclude();
        }
        else
        {
            currentLine++;
            speak();
        }
    }

    public virtual void Conclude()
    {
        currentLine = 0;
        man.setBar(false);
        man.setPlay(false);
        stopChat();
    }
}
