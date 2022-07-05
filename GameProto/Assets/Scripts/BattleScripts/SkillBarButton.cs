using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillBarButton : MonoBehaviour
{
    public BattleManager manager;
    public TMP_Text nom;
    public FightMath.Option type;

    private void Start()
    {
        manager = FindObjectOfType<BattleManager>();
    }

    public void Activate(FightMath.Option kind)
    {
        type = kind;
        switch(kind)
        {
            case FightMath.Option.ATTACK:
                nom.text = "Attack";
                break;
            case FightMath.Option.LIFE:
                nom.text = "Life";
                break;
        }
    }

    public void Use()
    {
        manager.ChoiceMade(type);
    }
}
