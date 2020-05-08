using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    private float secondsCount;
    private List<Vector3> playerLocations;

    [SerializeField]
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerLocations = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        if (secondsCount >= 2)
        {
            playerLocations.Add(playerTransform.position);
            secondsCount = 0;
        }
    }

    public void ClearLocations()
    {
        playerLocations.Clear();
    }

    //TO DO: Send to Ghost service
    public void PutLocationsInDatabase()
    {
        foreach (Vector3 pLoc in playerLocations)
        {
            Debug.Log("x: " + pLoc.x + " y: " + pLoc.y + " z: " + pLoc.z);
        }
        Debug.Log("TO DO: Send to Ghost Service");
    }

}
