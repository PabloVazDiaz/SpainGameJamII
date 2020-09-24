using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = Input.GetButton("Jump");

        if (Input.GetButtonDown("Fire1"))
        {
            playerController.Fire();
        }
    }

    private void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        playerController.Move(hAxis, jump);
    }
}
