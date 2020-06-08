using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerHistory
{
    [JsonProperty]
    public int playerLevelId;
    [JsonProperty]
    public int playerAccountId;
    [JsonProperty]
    public int Level;
    [JsonProperty]
    public string emailPlayer;
    [JsonProperty]
    public string userName;
    [JsonProperty]
    public List<PlayerLocation> playerLocations;

    [JsonConstructor]
    public PlayerHistory (int playerLevelId, int playerAccountId, int level, string emailPlayer, string userName, List<PlayerLocation> playerLocations)
    {
        this.playerLevelId = playerLevelId;
        this.playerAccountId = playerAccountId;
        this.Level = level;
        this.emailPlayer = emailPlayer;
        this.userName = userName;
        this.playerLocations = playerLocations;
    }

    public PlayerHistory(int playerAccountId, int level, string emailPlayer, string userName, List<PlayerLocation> playerLocations)
    {
        this.playerAccountId = playerAccountId;
        this.Level = level;
        this.emailPlayer = emailPlayer;
        this.userName = userName;
        this.playerLocations = playerLocations;
    }
}
