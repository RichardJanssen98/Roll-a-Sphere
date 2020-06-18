using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using Assets.Scripts.Account;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private int score = 0;
    HttpClient httpClient;

    private PlayerAccount loggedInPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        scoreText.text = "Score: " + score;
        httpClient = new HttpClient();

        //KILL ME NOW
        loggedInPlayer = GameObject.FindObjectOfType<AccountController>().loggedInPlayer;
    }

    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public async void PostScoreForCheatTest(Timer timer)
    {
        var values = new Dictionary<string, string>
        {
            { "thing1", "hello" },
            { "thing2", "world" }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await httpClient.PostAsync("http://34.120.156.223/api/cheat/levelminimums/amicheating?level=1&score=" + score + "&time=" + timer.GetTimerInSeconds(), content);
        

        var responseString = await response.Content.ReadAsStringAsync();

        bool responseBool = Boolean.Parse(responseString);

        if (responseBool)
        {
            AlertCheat();
        }
    }

    private void AlertCheat()
    {
        Debug.Log("YOU CHEATED, STOP THAT");
    }

    public async void PostScoreToDatabase(Timer timer)
    {
        var values = new Dictionary<string, string>
        {
            { "thing1", "hello" },
            { "thing2", "world" }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await httpClient.PostAsync("http://34.120.156.223/api/scorepost/playerScores/playerScore?playeraccountid=" + loggedInPlayer.AccountId + "&level=1&score=" + score + "&time=" + timer.GetTimerInSeconds() + "&emailPlayer=" + loggedInPlayer.Email + "&userName=" + loggedInPlayer.Username, content);
        Debug.Log("Score response: " + response);
    }
}

