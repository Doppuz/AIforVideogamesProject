using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBehaviour2 : MonoBehaviour{

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
    }

    void Update() {
        

    }
    
    void FixedUpdate() {

       
    }
    

    private void OnCollisionEnter(Collision collision) {

    if (collision.gameObject.tag == "Agent") {

            ContactPoint contact = collision.contacts[0];

            rg.velocity = new Vector3(0f, 0f, 0f);

            Vector3 destination = Vector3.Reflect(transform.localPosition,
                                      collision.contacts[0].normal);
            rg.AddForce(destination.normalized * speed,
                       ForceMode.VelocityChange);

            if (collision.gameObject.name == "Player" && behind != null) {
                if (behind.transform.localPosition.z > 0)
                    behind.transform.localPosition = new Vector3(behind.transform.localPosition.x, behind.transform.localPosition.y, Random.Range(1.5f, 4.7f));
                else
                    behind.transform.localPosition = new Vector3(behind.transform.localPosition.x, behind.transform.localPosition.y, Random.Range(-4.7f, -1.5f));
            }

        } 
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
