using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string level;
    public PlayerMovement playe;
    public GameManager man;
    public EnterRandomBattle battleman;

    void Awake()
    {
        playe = GetComponent<PlayerMovement>();
        man = FindObjectOfType<GameManager>();
        battleman = FindObjectOfType<EnterRandomBattle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getPlace()
    {
        return gameObject.transform.position;
    }

    public void immobile(bool immobile)
    {
        playe.enabled = !immobile;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Hazard")
        {
            battleman.inRange = true;
        }
    }
}
