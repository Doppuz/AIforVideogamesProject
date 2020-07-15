using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBehaviour : MonoBehaviour{

    public float speed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() {
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

    void FixedUpdate() {
        Vector3 destination = transform.forward;
        Rigidbody rb = GetComponent<Rigidbody>();
        Debug.Log(transform.position);
        rb.MovePosition(transform.position + destination * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("I think yes");
        if (collision.gameObject.tag == "Agent") {
            Debug.Log("Collision "+ Vector3.Reflect(transform.position, transform.right));
            // transform.position = Vector3.Reflect(transform.position,transform.right);
            transform.position = new Vector3(1f,1f,1f);
            //GetComponent<Rigidbody>().MovePosition(transform.position - transform.forward * speed*\ * Time.deltaTime);
        }
    }
}
