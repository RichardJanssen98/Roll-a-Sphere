using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText; 

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        scoreText.text = "Score: " + score;
    }

    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }
}
