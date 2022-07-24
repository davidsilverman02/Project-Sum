using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using NamedUnits;

namespace WeaknessCalculator
{
    [Serializable]
    public enum State { NORMAL = 0, RESIST = 1, WEAK = 2, IMMUNE = 3 }
    [Serializable]
    public enum DamageType { WEAPON = 0, FIRE = 1, WATER = 2, NATURE = 3, FORMATION = 4, TIME = 5, SPACE = 6, LIFE = 7, DEATH = 8}

    [Serializable]
    public class Weakness
    {
        //[NamedArray(typeof(DamageType))]
        public State[] effectiveness = new State[9];

        public Weakness()
        {

        }

        public void effectiveDamage(int raw, DamageType element, Unit user, Unit target, bool drain)
        {
            switch(effectiveness[(int)element])
            {
                case State.NORMAL:
                    target.TakeDamage(raw);
                    if(drain)
                    {
                        user.Restore(raw / 4);
                    }
                    break;
                case State.RESIST:
                    target.TakeDamage(raw / 2);
                    if (drain)
                    {
                        user.Restore(raw / 8);
                    }
                    break;
                case State.WEAK:
                    target.TakeDamage(raw * 2);
                    if (drain)
                    {
                        user.Restore(raw / 2);
                    }
                    break;
                case State.IMMUNE:
                    target.TakeDamage(0);
                    break;
                default:
                    break;
            }
        }

        public void setWeaknesses(Weakness weak)
        {
            effectiveness = weak.effectiveness;
        }
    }
}

namespace NamedUnits
{
    public class NamedArray : PropertyAttribute
    {
        public string[] names;
        public NamedArray(System.Type enumName)
        {
            this.names = System.Enum.GetNames(enumName);
        }
    }

    [CustomPropertyDrawer(typeof(NamedArray))]
    public class NamedArrayDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            NamedArray enumNames = attribute as NamedArray;
            int index = System.Convert.ToInt32(property.propertyPath.Substring(property.propertyPath.IndexOf("[")).Replace("[", "").Replace("]", ""));
            label.text = enumNames.names[index];
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}

