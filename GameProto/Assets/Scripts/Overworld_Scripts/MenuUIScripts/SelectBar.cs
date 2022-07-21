using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBar : MonoBehaviour
{
    public GameObject barElement;

    public MenuManager menu;
    public List<GameObject> notes;

    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<MenuManager>();

        for (int i = 0; i < menu.getPlayers().Count; i++)
        {
            GameObject spawn = Instantiate(barElement, gameObject.transform.position, Quaternion.identity);
            spawn.transform.SetParent(gameObject.transform);
            notes.Add(spawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < menu.getPlayers().Count; i++)
        {
            if(i == menu.selected)
            {
                notes[i].transform.localScale = new Vector3(2, 2);
            }
            else
            {
                notes[i].transform.localScale = new Vector3(1, 1);
            }
        }
    }
}
