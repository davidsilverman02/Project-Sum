using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StatSave;

public class GameManager : MonoBehaviour
{
    
    public GameObject player;
    public GameObject UI;
    public GameObject menu;

    public enum gameState {OVERWORLD, BATTLE, MENU}
    public gameState state;
    public Player ovPlayer;
    public OverworldUIManager ovUI;
    public MenuManager ovMenu;
    public int level;
    public int curLevel;
    public float spawnX;
    public float spawnY;
    public float spawnZ;
    public List<NPC> npcs;
    public NPC interact;
    public List<Chest> chests;
    public Chest closest;

    public bool playerMoving;
    public bool isTalk;
    public bool inMenu;
    public float isClose = 3f;
    public float canReach = 1f;

    public List<int> party;
    public List<StatContainer.StatObject> players;

    public List<bool> opened;

    public InventorySystem inventory;
    // Insert all players if switching through story

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();

        level = data.level;
        spawnX = data.playerX;
        spawnY = data.playerY;
        spawnZ = data.playerZ;

        party = data.group;
        players = data.stObjs;
        opened = data.opened;

        inventory = data.inventory;

        SceneManager.LoadScene(level);

        setChests();
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        setChests();
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

                    if ((!inMenu && isTalk == false) && Input.GetKeyDown(KeyCode.Return))
                    {
                        isTalk = true;
                        ovPlayer.immobile(true);
                        interact.Interact();

                    }
                    else if(!inMenu && Input.GetKeyDown(KeyCode.Return))
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

            if(chests.Count > 0)
            {
                if (getClosestChestToPlayer() != null)
                {
                    if (getClosestChestToPlayer() != closest)
                    {
                        /*
                        if (closest != null)
                        {
                            interact.canChat(false);
                        }
                        */
                        closest = getClosestChestToPlayer();
                    }

                    if ((!inMenu && isTalk == false) && Input.GetKeyDown(KeyCode.Return))
                    {
                        isTalk = true;
                        ovPlayer.immobile(true);
                        closest.Open();

                    }
                    else if (!inMenu && Input.GetKeyDown(KeyCode.Return))
                    {
                        interact.Continue();
                    }
                }
                else
                {
                    if (closest != null)
                    {
                        closest = null;
                    }
                }
            }

            if(Input.GetKeyDown(KeyCode.X))
            {
                if(!(inMenu && (party.Count < 1 || party.Count > 4)))
                {
                    inMenu = !inMenu;
                    ovMenu.setMenu(inMenu);
                }
            }
        }
        if(state == gameState.BATTLE)
        {
            playerMoving = false;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            SaveGame();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            LoadGame();
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
        chests.Clear();

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

            if(GameObject.Find("MenuUI") == null)
            {
                GameObject meni = Instantiate(menu, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            }
            ovMenu = FindObjectOfType<MenuManager>();
        }
        else
        {
            ovPlayer = null;
            ovUI = null;
            ovMenu = null;
            interact = null;
            closest = null;
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

    public void addChest(Chest toAdd)
    {
        chests.Add(toAdd);
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

    Chest getClosestChestToPlayer()
    {
        Chest closest = null;
        float minDis = Mathf.Infinity;
        float dis;

        foreach(Chest chest in chests)
        {
            dis = Vector3.Distance(ovPlayer.getPlace(), chest.getPos());
            if(dis < minDis && dis <= canReach)
            {
                closest = chest;
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

    public void setChests()
    {
        for(int i = 0; i < chests.Count; i++)
        {
            chests[i].isOpened = FightMath.getChestOpened(chests[i], opened);
        }
    }

    public void addItem(Item item)
    {
        inventory.Add(item);
    }
}
