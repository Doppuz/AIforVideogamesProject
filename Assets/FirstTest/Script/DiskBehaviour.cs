using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBehaviour : MonoBehaviour{

    public float speed;
    
    private Rigidbody rg;
    private Vector3 destination;
    public bool haveILose = false;
    
    void Start(){
        rg = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Agent") {
            
            ContactPoint contact = collision.contacts[0];

            rg.velocity = new Vector3(0f,0f,0f);
            Vector3 destination = Vector3.Reflect(transform.localPosition,
                                      collision.contacts[0].normal);
            rg.AddForce(destination.normalized * speed,
                       ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Goal")
            haveILose = true;
    }

}
