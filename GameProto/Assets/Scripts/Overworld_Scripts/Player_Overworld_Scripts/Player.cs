using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float xCord;
    public float yCord;
    public string level;
    public PlayerMovement playe;

    void Awake()
    {
        playe = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
