using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBar : MonoBehaviour
{
    public GameObject turnBarIcon;
    public Transform parent;
    public List<TurnBarUnit> icons;

    public void SetTurns(List<Unit> order, List<Unit> players)
    {
        foreach (TurnBarUnit tbu in icons)
        {
            Destroy(tbu.gameObject);
        }
        icons.Clear();

        foreach (Unit u in order)
        {
            TurnBarUnit newIcon = Instantiate(turnBarIcon, parent).GetComponent<TurnBarUnit>();
            bool ally = false;
            for (int i = 0; i < players.Count; i ++)
            {
                if (u == players[i])
                {
                    ally = true;
                    break;
                }
            }
            newIcon.Setup(ally, u);
            icons.Add(newIcon);
        }
    }
}
