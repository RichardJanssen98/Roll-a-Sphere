using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GhostManager ghostManager;
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void EndGame(bool win)
    {
        if (win)
        {
            scoreManager.PostScoreForCheatTest(timer);
            ghostManager.PostLocationsToDatabase();
            scoreManager.PostScoreToDatabase(timer);
            Destroy(GameObject.FindObjectOfType<SceneLoadManager>().gameObject);
            SceneManager.LoadScene(0);
        }
        else
        {
            ghostManager.ClearLocations();
            SceneManager.LoadScene(1);
        }
    }
}
