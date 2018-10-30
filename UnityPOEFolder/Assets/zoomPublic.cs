using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomPublic : MonoBehaviour
{
    public float panSpeed = 20f;
    public float border = 5.12f;
    public float zoom = 5.12f;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posistion = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - border)
        {
            posistion.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= border)
        {
            posistion.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= border)
        {
            posistion.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - border)
        {
            posistion.x += panSpeed * Time.deltaTime;
        }
        transform.position = posistion;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoom > 5)
            {
                zoom -= 1;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (zoom < 55)
            {
                zoom += 1;
            }
        }
        GetComponent<Camera>().orthographicSize = zoom;
    }
}
