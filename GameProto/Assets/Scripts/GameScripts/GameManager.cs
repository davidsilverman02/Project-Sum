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
    public int level;
    public int curLevel;
    public float spawnX;
    public float spawnY;
    public float spawnZ;

    public bool playerMoving;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
       
    }

    void Update()
    {
        if(state == gameState.OVERWORLD)
        {
            spawnX = ovPlayer.gameObject.transform.position.x;
            spawnY = ovPlayer.gameObject.transform.position.y;
            spawnZ = ovPlayer.gameObject.transform.position.z;
            level = SceneManager.GetActiveScene().buildIndex;
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        curLevel = SceneManager.GetActiveScene().buildIndex;

        switch (curLevel)
        {
            case 2:
                state = gameState.BATTLE;
                break;
            case 0:
                state = gameState.MENU;
                break;
            case 3:
                state = gameState.MENU;
                break;
            default:
                state = gameState.OVERWORLD;
                break;
        }
        if (state == gameState.OVERWORLD)
        {
            if (GameObject.Find("Player") == null)
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

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadLevel(int index, Vector3 pos)
    {
        SceneManager.LoadScene(index);

    }
}
