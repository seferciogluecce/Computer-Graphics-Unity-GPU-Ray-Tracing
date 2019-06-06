using System.Collections;
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
            Application.Quit();        }

    }


    void RotateWithASDQWE()
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

    private void MoveWithArrows()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(increment * Time.deltaTime, 0, 0);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-increment * Time.deltaTime, 0, 0);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0, increment * Time.deltaTime, 0);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0, -increment * Time.deltaTime, 0);

        }  if (Input.GetKey(KeyCode.Keypad1))
        {
            this.transform.Translate(0, 0, increment * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            this.transform.Translate(0, 0, -increment * Time.deltaTime);

        }
    }


}
