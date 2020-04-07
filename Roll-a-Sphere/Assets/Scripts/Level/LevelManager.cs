using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void EndGame()
    {
        Debug.Log("TO-DO: Add API call to Scoreboard");
        Debug.Log("TO-DO: Change scene to menu");
    }
}
