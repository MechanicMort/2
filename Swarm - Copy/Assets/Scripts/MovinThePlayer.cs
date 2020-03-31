using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovinThePlayer : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // added Time.deltaTime helps with frame rate and movement issues(Cameron)
        Vector3 movement = new Vector3(x, 0f, z) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + transform.TransformDirection(movement);
        rb.MovePosition(newPosition);
    }
}

