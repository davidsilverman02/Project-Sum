using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoad : MonoBehaviour
{
    public string tri;

    public void Load()
    {
        SceneManager.LoadScene(tri);
    }
}
