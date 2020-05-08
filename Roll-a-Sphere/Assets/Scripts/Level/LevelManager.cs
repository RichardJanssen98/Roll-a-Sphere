using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GhostManager ghostManager;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void EndGame(bool win)
    {
        if (win)
        {
            ghostManager.PutLocationsInDatabase();
            Debug.Log("TO-DO: Add API call to Scoreboard");
            Debug.Log("TO-DO: Change scene to menu");
            SceneManager.LoadScene(0);
        }
        else
        {
            ghostManager.ClearLocations();
            SceneManager.LoadScene(1);
        }
    }
}
