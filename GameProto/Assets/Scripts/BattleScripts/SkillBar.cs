using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBar : MonoBehaviour
{
    const int SIZE = 4;

    public SkillBarButton[] barButtons = new SkillBarButton[SIZE];

    public void SetSkills(Hero basis)
    {
        for(int i = 0; i < SIZE; i++)
        {
            if(basis.GetOptions()[i] == FightMath.Option.NOTHING)
            {
                
            }
            else
            {

            }
        }
    }
}
