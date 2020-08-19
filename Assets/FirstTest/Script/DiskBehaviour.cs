using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBehaviour : MonoBehaviour{

    public float speed;


    private Vector3 direction;
    private Rigidbody rg;
    private Vector3 destination;
    private Vector3 normal = Vector3.zero;
    private Vector3 lastHittedPoint;
    public bool haveILose = false;
    
    void Start(){
        rg = GetComponent<Rigidbody>();
    }

    void Update() {


    }
    
    void FixedUpdate() {

       
    }
    

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Agent") {
            
            ContactPoint contact = collision.contacts[0];

            //           if (speed < 20)
            //               speed += 2f;

            rg.velocity = new Vector3(0f,0f,0f);
            float z = Random.Range(-2f, +2f);
            Vector3 destination = Vector3.Reflect(transform.localPosition,
                                      collision.contacts[0].normal) + new Vector3(0,0,z);
            rg.AddForce(destination.normalized * speed,
                       ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Goal")
            haveILose = true;
    }

}
