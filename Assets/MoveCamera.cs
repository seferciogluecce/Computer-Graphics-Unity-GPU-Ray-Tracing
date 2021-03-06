﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public class MoveCamera : MonoBehaviour
{

    public int increment = 44;

    void Update()
    {
        MoveWithArrows();
        RotateWithASDQWE();

        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

    }

    void Translate(float x, float y, float z) //point transformation
    {
        this.transform.position += new Vector3(x, y, z);
    }


    void RotateWithASDQWE() //Unity built in rotations
    {
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(0, 0, increment * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(0, 0, -increment * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(0, increment * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(0, -increment * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Rotate(increment * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Rotate(-increment * Time.deltaTime, 0, 0);
        }

    }

    private void MoveWithArrows()//point transformation
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Translate(0, 0, increment * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Translate(0, 0, -increment * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Translate(0, increment * Time.deltaTime, 0);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
           Translate(0, -increment * Time.deltaTime, 0);

        }
        if (Input.GetKey("[1]") || Input.GetKey("1"))
        {
            Translate(-increment * Time.deltaTime, 0, 0);

        }
        if (Input.GetKey("[2]") || Input.GetKey("2"))           
        {
            Translate(increment * Time.deltaTime, 0, 0);

        }
    }


}
