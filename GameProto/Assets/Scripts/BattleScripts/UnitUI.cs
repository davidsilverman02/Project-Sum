using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUI : MonoBehaviour
{
    public GameObject selected;
    public GameObject inUse;

    public void Start()
    {
        Reset();
    }

    public void SetTarget(bool active)
    {
        selected.SetActive(active);
    }

    public void Reset()
    {
        SetTarget(false);
    }
}
