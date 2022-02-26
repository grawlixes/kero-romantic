using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GFController : MonoBehaviour
{
    private int loadLevel;
    private void Start()
    {
        string name = SceneManager.GetActiveScene().name;
        loadLevel = (int.Parse(name.Substring(5))) + 1;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "Character_Frog")
            SceneManager.LoadScene(loadLevel);
    }
}
