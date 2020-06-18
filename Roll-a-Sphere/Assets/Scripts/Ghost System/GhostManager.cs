using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;
using Assets.Scripts.Account;

public class GhostManager : MonoBehaviour
{
    private float secondsCount;
    private int locationNumber = 1;
    private List<PlayerLocation> playerLocations;

    [SerializeField]
    private Transform playerTransform;

    HttpClient httpClient;

    private List<PlayerHistory> playerHistories;

    [SerializeField]
    private GameObject ghostButtonListParent;
    [SerializeField]
    private GameObject buttonToInstantiate;
    [SerializeField]
    private Transform ghostPlayerObjectSpawnPoint;
    [SerializeField]
    private GameObject ghostPlayerObject;

    private float ghostMoveTimer;

    private PlayerAccount loggedInPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerLocations = new List<PlayerLocation>();
        httpClient = new HttpClient();

        //KILL ME NOW
        loggedInPlayer = GameObject.FindObjectOfType<AccountController>().loggedInPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        if (secondsCount >= 1)
        {
            playerLocations.Add(new PlayerLocation(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z, locationNumber));
            locationNumber++;
            secondsCount = 0;
        }
    }

    public void ClearLocations()
    {
        playerLocations.Clear();
    }

    //TO DO: Send to Ghost service
    public async void PostLocationsToDatabase()
    {
        //var response = await httpClient.PostAsync("http://34.120.156.223/ghost/playerlevels/postLocations?level=1&emailPlayer=Richard@Richard.com&userName=Richard", JsonUtility.ToJson(playerLocations));
        PlayerHistory playerHistory = new PlayerHistory(loggedInPlayer.AccountId, 1, loggedInPlayer.Email, loggedInPlayer.Username, playerLocations);

        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://34.120.156.223/ghost/playerlevels/postLocations");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            Debug.Log("playerHistory: " + playerHistory.Level + " " + playerHistory.emailPlayer + " " + playerHistory.userName + " " + playerHistory.playerLocations.Count);
            string json = JsonUtility.ToJson(playerHistory);
            Debug.Log("json: " + json);
            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
        }
    }

    public async void GetLocationsFromDatabase()
    {
        var response = await httpClient.GetAsync("http://34.120.156.223/ghost/playerlevels");
        string jsonResponse = await response.Content.ReadAsStringAsync();

        playerHistories = JsonConvert.DeserializeObject<List<PlayerHistory>>(jsonResponse);

        FillGhostList();
    }

    public void FillGhostList()
    {
        foreach (PlayerHistory playerHistory in playerHistories)
        {
            GameObject newButton = Instantiate(buttonToInstantiate, ghostButtonListParent.transform);
            newButton.GetComponentInChildren<Text>().text = playerHistory.playerLevelId.ToString() + " " + playerHistory.userName;

            newButton.GetComponent<Button>().onClick.AddListener(delegate { StartGhostOnClick(playerHistory.playerLevelId); });
        }
    }

    private void StartGhostOnClick(int historyId)
    {
        foreach (PlayerHistory history in playerHistories)
        {
            if (history.playerLevelId == historyId)
            {
                StartCoroutine(SimulateGhostPlayer(history));
                break;
            }
        }
    }

    private IEnumerator SimulateGhostPlayer(PlayerHistory playerHistory)
    {
        GameObject ghostPlayer = Instantiate(ghostPlayerObject, ghostPlayerObjectSpawnPoint);
        Transform ghostPlayerTransform = ghostPlayer.GetComponent<Transform>();

        while (true)
        {
            foreach (PlayerLocation location in playerHistory.playerLocations)
            {
                Vector3 vectorLocation = new Vector3(location.x, location.y, location.z);
                Vector3 startPosition = ghostPlayerTransform.position;
                ghostMoveTimer = 0;

                while (ghostPlayerTransform.position != vectorLocation)
                {
                    ghostMoveTimer += Time.deltaTime / 1;
                    ghostPlayerTransform.position = Vector3.Lerp(startPosition, vectorLocation, ghostMoveTimer);
                    yield return null;
                }
            }
        }
    }
}
