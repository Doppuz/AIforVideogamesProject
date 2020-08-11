using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBehaviour2 : MonoBehaviour{

    public float speed;


    private Vector3 direction;
    private Rigidbody rg;
    private Vector3 destination;
    private Vector3 normal = Vector3.zero;
    private Vector3 lastHittedPoint;
    public bool haveILose = false;
    
    void Start(){
        rg = GetComponent<Rigidbody>();
        //destination = agent1.position;
        //rg.velocity = destination.normalized * speed;
        //rg.velocity = Vector3.forward * 8;
    }

    void Update() {


    }
    
    void FixedUpdate() {

       
    }
    

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Agent") {
            
            //Debug.Log("22 "+rg.isKinematic);
            //rg.isKinematic = true;
            ContactPoint contact = collision.contacts[0];

            //          Debug.Log(contact.point+" "+contact.normal);

            // reflect our old velocity off the contact point's normal vector
                        //destination = Vector3.Reflect(transform.localPosition, contact.normal);

                   //destination = new Vector3(destination.x, transform.localPosition.y, destination.z);

            //           if (speed < 20)
            //               speed += 2f;

            //rg.MovePosition(transform.position + destination.normalized * 15);
            //rg.velocity = destination.normalized * 22;
            //rg.velocity = Vector3.Reflect(transform.position,
            //                          collision.contacts[0].normal).normalized * 15;
            rg.velocity = new Vector3(0f,0f,0f);
            //rg.isKinematic = false;
            //float z = Random.Range(-4f, +4f);
            Vector3 destination = Vector3.Reflect(transform.localPosition,
                                      collision.contacts[0].normal);
            rg.AddForce(destination.normalized * speed,
                       ForceMode.VelocityChange);

            if(collision.gameObject.GetComponent<Rigidbody>() != null)
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Goal")
            haveILose = true;
        else if (other.gameObject.tag == "Movement")
            other.gameObject.transform.parent.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnDrawGizmos() {
        /*Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, normal);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, destination);*/
    }
}
