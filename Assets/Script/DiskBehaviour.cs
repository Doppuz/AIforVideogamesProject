using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBehaviour : MonoBehaviour{

    public float speed = 5f;
    public Transform agent1;
    public Transform agent2;

    private Vector3 direction;
    private Rigidbody rg;
    private Vector3 destination;
    private Vector3 normal = Vector3.zero;
    private Vector3 lastHittedPoint;
    private bool haveILose = false;
    
    void Start(){
        rg = GetComponent<Rigidbody>();
        destination = agent1.position;
        rg.velocity = destination.normalized * speed;
    }

    void Update() {


    }
    
    void FixedUpdate() {

       
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Agent") {
            ContactPoint contact = collision.contacts[0];

            normal = collision.contacts[0].normal;

            // reflect our old velocity off the contact point's normal vector
            destination = Vector3.Reflect(transform.position, contact.normal);

            destination = new Vector3(destination.x, transform.position.y, destination.z);

            if(speed < 24)
                speed += 2f;

            rg.velocity = destination.normalized * speed;

            Debug.Log(rg.velocity);
        }
    }

    private void OnTriggerEnter(Collider other) {
        haveILose = true;
        Debug.Log(haveILose);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, normal);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, destination);
    }
}
