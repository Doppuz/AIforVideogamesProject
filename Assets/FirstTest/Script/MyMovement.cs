using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMovement : MonoBehaviour
{

    public int speed = 10;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 pos = transform.position;

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


        transform.position = pos;*/
        /*Vector3 movedir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        print(movedir);
        GetComponent<Rigidbody>().AddForce(movedir * 20);*/

        // Get Horizontal and Vertical Input
        Vector3 movedir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + movedir * speed * Time.deltaTime);


    }
}
