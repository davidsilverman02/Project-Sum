using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRect : MonoBehaviour
{
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getPos()
    {
        return pos;
    }
}
