using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;


    public enum gameState {OVERWORLD, BATTLE, MENU}
    public gameState state;
    public Player ovPlayer;
    public string level;
    public string curLevel;
    public float spawnX;
    public float spawnY;
    public float spawnZ;

    public bool playerMoving;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        curLevel = SceneManager.GetActiveScene().name;

        switch (curLevel)
        {
            case "FightScene":
                state = gameState.BATTLE;
                break;
            case "TitleScreen":
                state = gameState.MENU;
                break;
            default:
                state = gameState.OVERWORLD;
                break;
        }

        if (state == gameState.OVERWORLD)
        {
            if(GameObject.Find("Player") == null)
            {
               GameObject plaer = Instantiate(player, new Vector3(spawnX, spawnY, spawnZ), gameObject.transform.rotation) as GameObject;
            }
            ovPlayer = FindObjectOfType<Player>();
        }
        else
        {
            ovPlayer = null;
        }
    }

    void Update()
    {
        if(state == gameState.OVERWORLD)
        {
            spawnX = ovPlayer.gameObject.transform.position.x;
            spawnY = ovPlayer.gameObject.transform.position.y;
            spawnZ = ovPlayer.gameObject.transform.position.z;
            level = SceneManager.GetActiveScene().name;
            if(ovPlayer != null)
            {
                if((ovPlayer.playe.movingX != 0 || ovPlayer.playe.movingY != 0) || ovPlayer.playe.movingZ != 0)
                {
                    playerMoving = true;
                }
                else
                {
                    playerMoving = false;
                }
            }
            else
            {
                playerMoving = false;
            }
        }

        if(state == gameState.BATTLE)
        {
            playerMoving = false;
        }
    }

    void OnApplicationQuit()
    {
        Destroy(this.gameObject);
    }

    public void LoadBack()
    {
        SceneManager.LoadScene(level);
    }
}
