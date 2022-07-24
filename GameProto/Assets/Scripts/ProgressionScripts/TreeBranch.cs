using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreeBranch : MonoBehaviour
{
    public bool active;

    public int cost;

    public TreeBranch up;
    public TreeBranch left;
    public TreeBranch down;
    public TreeBranch right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
