﻿using System.Collections;
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

    [SerializeField]
    private LevelManager levelManager;

    public int Score { get; set; }
    HttpClient httpClient;

    private PlayerAccount loggedInPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        scoreText.text = "Score: " + Score;
        httpClient = new HttpClient();

        //KILL ME NOW
        loggedInPlayer = GameObject.FindObjectOfType<AccountController>().loggedInPlayer;
    }

    public void IncreaseScore(int value)
    {
        Score += value;
        scoreText.text = "Score: " + Score;
    }

    public async void PostScoreForCheatTest(Timer timer)
    {
        var values = new Dictionary<string, string>
        {
            { "thing1", "hello" },
            { "thing2", "world" }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await httpClient.PostAsync("http://localhost:27015/cheat/levelminimums/amicheating?level=1&score=" + Score + "&time=" + timer.GetTimerInSeconds(), content);
        

        var responseString = await response.Content.ReadAsStringAsync();

        bool responseBool = Boolean.Parse(responseString);

        if (responseBool)
        {
            AlertCheat();
        }
        else
        {
            levelManager.PostScoreAndLocations();
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

        var response = await httpClient.PostAsync("http://localhost:27015/scorepost/playerScores/playerScore?playeraccountid=" + loggedInPlayer.AccountId + "&level=1&score=" + Score + "&time=" + timer.GetTimerInSeconds() + "&emailPlayer=" + loggedInPlayer.Email + "&userName=" + loggedInPlayer.Username, content);
        Debug.Log("Score response: " + response);
    }
}

