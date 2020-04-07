using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbody;

    private float speed = 1;
    private float jumpForce = 10;

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

        if (Input.GetAxis("Horizontal") < 0)
        {
            rigidbody.AddForce(new Vector3(-speed, 0, 0));
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            rigidbody.AddForce(new Vector3(speed, 0, 0));
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            rigidbody.AddForce(new Vector3(0, 0, -speed));
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            rigidbody.AddForce(new Vector3(0, 0, speed));
        }

        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            rigidbody.AddForce(0, jumpForce, 0);
        }
    }
}
