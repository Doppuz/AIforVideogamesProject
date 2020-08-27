using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskBehaviour3 : MonoBehaviour{

    public float speed;
    public GameObject behind;
    
    private Rigidbody rg;
    private Vector3 destination;
    private int i = 0;
    public bool haveILose = false;
    
    void Start(){
        rg = GetComponent<Rigidbody>();
    }

    void Update() {
        if (rg.velocity.magnitude < 5)
            rg.AddForce((rg.velocity.normalized + new Vector3(0.1f,0.1f,0.1f)) * speed);
    }
    
    void FixedUpdate() {

       
    }
    

    private void OnCollisionEnter(Collision collision) {

    if (collision.gameObject.tag == "Agent") {
            
            ContactPoint contact = collision.contacts[0];
            
            rg.velocity = new Vector3(0f, 0f, 0f);
           
            destination = Vector3.Reflect(transform.localPosition,
                      collision.contacts[0].normal);

                rg.AddForce((destination.normalized) * speed,
                       ForceMode.VelocityChange);  

            if (collision.gameObject.name == "Player" && behind != null) {
                if (behind.transform.localPosition.z > 0)
                    behind.transform.localPosition = new Vector3(behind.transform.localPosition.x, behind.transform.localPosition.y, Random.Range(4f, 8.5f));
                else
                    behind.transform.localPosition = new Vector3(behind.transform.localPosition.x, behind.transform.localPosition.y, Random.Range(-8.5f, -4f));
            }

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Goal")
            haveILose = true;
    }

}
