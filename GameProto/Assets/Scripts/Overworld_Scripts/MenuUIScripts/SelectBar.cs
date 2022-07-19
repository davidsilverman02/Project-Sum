using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBar : MonoBehaviour
{
    public GameObject barElement;

    public MenuManager menu;
    public List<GameObject> notes;
    public GameObject position;

    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<MenuManager>();

        for (int i = 0; i < menu.getPlayers().Count; i++)
        {
            GameObject spawn = Instantiate(barElement, position.transform.position, Quaternion.identity);
            notes.Add(spawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < menu.getPlayers().Count; i++)
        {

        }
    }
}
