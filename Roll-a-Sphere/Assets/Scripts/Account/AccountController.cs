using Assets.Scripts.Account;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

public class AccountController : MonoBehaviour
{
    //Register
    [SerializeField]
    private InputField emailInput;
    [SerializeField]
    private InputField passwordInput;
    [SerializeField]
    private InputField userNameInput;

    //Login
    [SerializeField]
    private InputField loginEmailInput;
    [SerializeField]
    private InputField loginPasswordInput;

    //ChangeName
    [SerializeField]
    private InputField changeUsernameInput;

    public PlayerAccount loggedInPlayer;

    public int PlayerId = 0;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void RegisterPlayer()
    {
        PlayerAccount playerAccount = new PlayerAccount(emailInput.text, userNameInput.text, passwordInput.text);

        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://34.120.156.223/api/account/accounts");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(playerAccount);
            Debug.Log("json: " + json);
            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
        }
    }

    public void LoginPlayerHehe()
    {
        LoginPlayerAsync();
    }

    public async System.Threading.Tasks.Task LoginPlayerAsync()
    {
        HttpClient httpClient = new HttpClient();

        var response = await httpClient.GetAsync("http://34.120.156.223/api/account/Accounts/Login?username=" + loginEmailInput.text + "&password=" + loginPasswordInput.text);
        string jsonResponse = await response.Content.ReadAsStringAsync();
    
        loggedInPlayer = JsonConvert.DeserializeObject<PlayerAccount>(jsonResponse);
        PlayerId = loggedInPlayer.AccountId;
    }

    public void DeletePlayer()
    {
        DeletePlayerAsync();
    }

    public async System.Threading.Tasks.Task DeletePlayerAsync()
    {
        HttpClient httpClient = new HttpClient();

        await httpClient.DeleteAsync("http://34.120.156.223/account/Accounts/" + PlayerId);
    }

    public void ChangeUsername()
    {
        ChangeUsernamePlayerAsync();
    }

    public async System.Threading.Tasks.Task ChangeUsernamePlayerAsync()
    {
        loggedInPlayer.Username = changeUsernameInput.text;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://34.120.156.223/account/accounts/" + loggedInPlayer.AccountId);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "PUT";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            Debug.Log("playerAccountChangeName: " + loggedInPlayer.Username);
            string json = JsonUtility.ToJson(loggedInPlayer);
            Debug.Log("json: " + json);
            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
        }
    }

    public void StartGame()
    {
        //Check if player is logged in
        if (loggedInPlayer.Username == loginEmailInput.text)
        {
            SceneLoadManager.LoadScene(1);
        }
    }
}
