using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUI : MonoBehaviour
{
    public GameObject selected;

    public void Start()
    {
        Reset();
    }

    public void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
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
