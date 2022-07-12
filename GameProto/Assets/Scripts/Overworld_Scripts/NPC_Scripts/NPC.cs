using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public NPCDisplay display;

    public GameManager man;

    public bool isTalking;

    // Start is called before the first frame update
    void Start()
    {
        man = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {

    }
}
