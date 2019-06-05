using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3D : MonoBehaviour
{
    public int increment = 44;
    float sensitivity = 22;
    float minFov = 15;
    float maxFov = 90;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(0, 0, increment * Time.deltaTime);
        } if (Input.GetKey(KeyCode.E))
        {
            this.transform.Rotate(0, 0, -increment * Time.deltaTime);
        } if (Input.GetKey(KeyCode.W))
        {
            this.transform.Rotate(0, increment * Time.deltaTime,0 );
        } if (Input.GetKey(KeyCode.S))
        {
            this.transform.Rotate(0,  -increment * Time.deltaTime,0);
        } if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate( increment * Time.deltaTime,0,  0);
        } if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(- increment * Time.deltaTime,0, 0);
        }

     

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;




    }
}
