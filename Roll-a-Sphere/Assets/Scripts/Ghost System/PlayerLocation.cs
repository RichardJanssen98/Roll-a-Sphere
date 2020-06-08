using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerLocation
{
    [JsonProperty]
    public int playerLocationId;
    [JsonProperty]
    public float x;
    [JsonProperty]
    public float y;
    [JsonProperty]
    public float z;
    [JsonProperty]
    public int locationNumber;

    [JsonConstructor]
    public PlayerLocation(int playerLocationId, float x, float y, float z, int locationNumber)
    {
        this.playerLocationId = playerLocationId;
        this.x = x;
        this.y = y;
        this.z = z;
        this.locationNumber = locationNumber;
    }

    public PlayerLocation( float x, float y, float z, int locationNumber)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.locationNumber = locationNumber;
    }
}
