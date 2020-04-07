using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private int scoreValue = 10;

    // Update is called once per frame
    void Update()
    {
        //Spin around
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            other.GetComponent<PlayerMovement>().CallIncreaseScore(scoreValue);
            gameObject.SetActive(false);
        }
    }
}
