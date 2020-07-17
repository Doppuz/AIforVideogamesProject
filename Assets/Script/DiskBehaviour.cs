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
    
    void Start(){
        rg = GetComponent<Rigidbody>();
        destination = agent1.position;
        //lastHittedPoint = transform.position;
        //rg.velocity = Vector3.forward * speed;
        rg.velocity = destination.normalized * speed;
    }

    void Update() {


        /*Vector3 pos = transform.position;

        //transform.Translate(destination * speed * Time.deltaTime);

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

    }

    /*3bool m_oneTime = false;

    void FixedUpdate() {
        //Vector3 destination = transform.forward;
        //Debug.Log(transform.position);
        //rb.MovePosition(transform.position + destination * speed * Time.deltaTime);
        //rg.AddForce(agent1.position * speed);

        //Debug.DrawLine(transform.position,  destination , Color.blue);
        //rg.MovePosition(transform.position + destination * speed * Time.deltaTime);
        //rg.AddForce(destination * speed);
        if (!m_oneTime) {
            GetComponent<Rigidbody>().AddForce(Vector3.forward*speed, ForceMode.VelocityChange);
            m_oneTime = true;
        }
    }*/


    /*private void OnCollisionEnter(Collision collision) {
        Debug.Log("I think yes");
        if (collision.gameObject.tag == "Agent" | collision.gameObject.tag == "Wall") {
            // transform.position = Vector3.Reflect(transform.position,transform.right);
            //Debug.DrawLine(Vector3.zero, Vector3.back, Color.black, 10f);
            //transform.position = Vector3.Reflect(transform.position, Vector3.left);
            //speed = -speed;
            //GetComponent<Rigidbody>().MovePosition(transform.position - transform.forward * speed*\ * Time.deltaTime);
            //Vector3 tmpdir = Vector3.Reflect(transform.position, collision.contacts[0].normal);
            //Debug.DrawLine(transform.position, Vector3.Reflect(transform.position, collision.contacts[0].normal), Color.green);
            //destination = tmpdir.normalized;
            //rg.AddForce(direction * speed);
            //rg.AddForce(Vector3.Reflect(transform.position, collision.contacts[0].normal) * speed, ForceMode.VelocityChange);
            normal = collision.GetContact(0).normal;
            Debug.Log("A " + collision.contacts[0].normal);
            Debug.Log("AA " + destination);
            Debug.DrawLine(transform.position, Vector3.Reflect(transform.position, collision.contacts[0].normal), Color.green);
            destination = Vector3.Reflect((lastHittedPoint - transform.position).normalized, collision.contacts[3].normal);
            Debug.DrawLine(transform.position,destination, Color.blue);
            lastHittedPoint = transform.position;
            print(collision.contacts.Length);

        } else if (collision.gameObject.tag == "WallLeft") {
            //Vector3 tmpdir = Vector3.Reflect(transform.position, collision.contacts[0].normal);
            //Debug.DrawLine(transform.position, Vector3.Reflect(transform.position, collision.contacts[0].normal), Color.green);
            //destination = tmpdir.normalized;
            //destination = Vector3.Reflect(transform.position, new Vector3(9.15f, 0,0)).normalized;
            //rg.AddForce(Vector3.Reflect(transform.position, collision.contacts[0].normal) * speed, ForceMode.VelocityChange);
        }
    }*/
    
    void FixedUpdate() {

       
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Agent") {
            ContactPoint contact = collision.contacts[0];

            normal = collision.contacts[0].normal;

            // reflect our old velocity off the contact point's normal vector
            destination = Vector3.Reflect(transform.position, contact.normal);

            if(speed < 15)
                speed += 2f;

            rg.velocity = destination.normalized * speed;
            //rg.velocity = destination;


        } else if(collision.gameObject.tag == "Wall") {
            //rg.AddForce(destination.normalized * speed, ForceMode.VelocityChange);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, normal);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, destination);
    }
}
