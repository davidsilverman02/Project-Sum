using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterRandomBattle : MonoBehaviour
{
    public int eNumerator;
    public int eDenominator;
    public bool inRange;
    public GameManager manager;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        int rand = Random.Range(1, eDenominator);
        if((rand <= eNumerator && manager.playerMoving == true) && inRange)
        {
            SceneManager.LoadScene("FightScene");
        }
    }
}
