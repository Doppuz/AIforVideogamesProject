﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBehaviour3 : MonoBehaviour{

    public float speed;
    public GameObject behind;

    private Vector3 direction;
    private Rigidbody rg;
    private Vector3 destination;
    private Vector3 normal = Vector3.zero;
    private Vector3 lastHittedPoint;
    private Vector3 lastPosition = Vector3.zero;
    private int i = 0;
    public bool haveILose = false;
    
    void Start(){
        rg = GetComponent<Rigidbody>();
        //destination = agent1.position;
        //rg.velocity = destination.normalized * speed;
        //rg.velocity = Vector3.forward * 8;
    }

    void Update() {
        
        if (rg.velocity.magnitude < 5)
            rg.AddForce((rg.velocity.normalized + new Vector3(0.1f,0.1f,0.1f)) * speed);
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

                       //if (speed < 20)
                         //  speed += 2f;

            //rg.MovePosition(transform.position + destination.normalized * 15);
            //rg.velocity = destination.normalized * 22;
            //rg.velocity = Vector3.Reflect(transform.position,
            //                          collision.contacts[0].normal).normalized * 15;
            
            rg.velocity = new Vector3(0f, 0f, 0f);
            //rg.isKinematic = false;
            //float z = Random.Range(-4f, +4f);
            Vector3 destination;
                destination = Vector3.Reflect(transform.localPosition,
                                      collision.contacts[0].normal);

            rg.AddForce(destination.normalized * speed,
                       ForceMode.VelocityChange);

            if (collision.gameObject.name == "Player" && behind != null) {
                if (behind.transform.localPosition.z > 0)
                    behind.transform.localPosition = new Vector3(behind.transform.localPosition.x, behind.transform.localPosition.y, Random.Range(-0.8f, 4.7f));
                else
                    behind.transform.localPosition = new Vector3(behind.transform.localPosition.x, behind.transform.localPosition.y, Random.Range(-8.5f, -3f));
            }

        } /*else if (collision.gameObject.tag == "Side") {
            if (lastPosition != Vector3.zero) {
                if (i == 5) {
                    rg.velocity = Vector3.zero;
                    rg.AddForce(new Vector3(behind.transform.localPosition.x, behind.transform.localPosition.y, behind.transform.localPosition.z).normalized * speed);

                    lastPosition = Vector3.zero;
                    i = 0;
                    Debug.Log("Added force");
                } else {
                    float deltaZ = Mathf.Abs(lastPosition.z - transform.position.z);

                    // Debug.Log(deltaZ);
                    if (deltaZ < 1f)
                        i += 1;
                    else {
                        i = 0;
                        lastPosition = Vector3.zero;
                    }
                }
            }

            if (lastPosition == Vector3.zero) {
                lastPosition = transform.position;
                i = 0;
            }
        }*/
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Goal")
            haveILose = true;
    }

    private void OnDrawGizmos() {
        /*Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, normal);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, destination);*/
    }
}
