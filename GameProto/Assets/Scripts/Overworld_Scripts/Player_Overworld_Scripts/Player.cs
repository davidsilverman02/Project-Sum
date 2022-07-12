using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string level;
    public PlayerMovement playe;
    public GameManager man;

    void Awake()
    {
        playe = GetComponent<PlayerMovement>();
        man = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
