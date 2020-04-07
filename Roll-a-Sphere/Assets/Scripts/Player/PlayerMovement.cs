using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbody;

    [SerializeField]
    private ScoreManager scoreManager;

    private float speed = 1;
    private float jumpingForce = 10f;

    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(gameObject.transform.position, new Vector3(0, -1, 0), 1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Left (A)
        if (Input.GetAxis("Horizontal") < 0)
        {
            rigidbody.AddForce(new Vector3(-speed, 0, 0));
        }
        //Right (D)
        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidbody.AddForce(new Vector3(speed, 0, 0));
        }
        //Backward (S)
        if (Input.GetAxis("Vertical") < 0)
        {
            rigidbody.AddForce(new Vector3(0, 0, -speed));
        }
        //Forward (W)
        if (Input.GetAxis("Vertical") > 0)
        {
            rigidbody.AddForce(new Vector3(0, 0, speed));
        }

        //Jump (Space)
        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            rigidbody.AddForce(new Vector3(0, jumpingForce, 0));
        }
    }

    public void CallIncreaseScore(int scoreValue)
    {
        scoreManager.IncreaseScore(scoreValue);
    }
}