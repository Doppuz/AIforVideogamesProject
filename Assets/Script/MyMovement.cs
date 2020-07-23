using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMovement : MonoBehaviour
{

    public int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w")) {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("s")) {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d")) {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            pos.x -= speed * Time.deltaTime;
        }


        transform.position = pos;
    }
}
