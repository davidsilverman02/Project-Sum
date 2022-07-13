using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject UI;


    public enum gameState {OVERWORLD, BATTLE, MENU}
    public gameState state;
    public Player ovPlayer;
    public OverworldUIManager ovUI;
    public int level;
    public int curLevel;
    public float spawnX;
    public float spawnY;
    public float spawnZ;
    public List<NPC> npcs;
    public NPC interact;

    public bool playerMoving;
    public bool isTalk;
    public float isClose = 3f;

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

            if(npcs.Count > 0)
            {
                if (getClosestNPCToPlayer() != null)
                {
                    if (getClosestNPCToPlayer() != interact)
                    {
                        if(interact != null)
                        {
                            interact.canChat(false);
                        }
                        interact = getClosestNPCToPlayer();
                    }

                    if (isTalk == false && Input.GetKeyDown(KeyCode.Return))
                    {
                        isTalk = true;
                        ovPlayer.immobile(true);
                        interact.Interact();

                    }
                    else if(Input.GetKeyDown(KeyCode.Return))
                    {
                        interact.Continue();
                    }
                    else if(isTalk == false)
                    {
                        interact.canChat(true);
                    }
                }
                else
                {
                    if(interact != null)
                    {
                        interact.canChat(false);
                        interact = null;
                    }
                }
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

        npcs.Clear();

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

            if(GameObject.Find("OverworldUI") == null)
            {
                GameObject ui = Instantiate(UI, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            }
            ovUI = FindObjectOfType<OverworldUIManager>();
        }
        else
        {
            ovPlayer = null;
            ovUI = null;
            interact = null;
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

    public void addNPC(NPC toAdd)
    {
        npcs.Add(toAdd);
    }

    NPC getClosestNPCToPlayer()
    {
        NPC closest = null;
        float minDis = Mathf.Infinity;
        float dis;

        foreach(NPC npc in npcs)
        {
            dis = Vector3.Distance(ovPlayer.getPlace(), npc.getPos());
            if(dis < minDis && dis <= isClose)
            {
                closest = npc;
                minDis = dis;
            }
        }

        return closest;
    }

    public void setBar(bool on)
    {
        ovUI.setChatBar(on);
    }

    public void setUIChat(string message)
    {
        ovUI.setText(message);
    }

    public void setPlay(bool set)
    {
        ovPlayer.immobile(set);
    }
}
