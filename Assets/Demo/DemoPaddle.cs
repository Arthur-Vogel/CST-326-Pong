using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DemoPaddle : MonoBehaviour
{
    public float speed = 5f;
    public bool isPlayerOne; 
    
    private int z_max = 5;
    private int z_min = -5;

    private void Update()
    {
        float moveInput = 0f;

        if (isPlayerOne && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            moveInput = Input.GetAxis("LeftPlayer"); 
        }
        else if(!isPlayerOne && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            moveInput = Input.GetAxis("RightPlayer");
        }
        

        Vector3 movement = new Vector3(0, 0, moveInput) * speed * Time.deltaTime;
        transform.Translate(movement);
        if(transform.position.z > z_max)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z_max);
        }
        if(transform.position.z < z_min)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z_min);
        }
    }

    
}
