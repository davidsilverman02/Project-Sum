using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDisplay : MonoBehaviour
{
    public GameObject selected;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
    }

    public void Reset()
    {
        setCanChat(false);
    }

    public void setCanChat(bool chat)
    {
        selected.SetActive(chat);
    }
}
