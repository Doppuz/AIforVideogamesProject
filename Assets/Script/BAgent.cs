using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BAgent : Agent{
    [Header("Agent parameter")]

    public GameObject hockeyDisk;
    public GameObject field;
    public float agentSpeed = 7f;

    private Rigidbody diskRB;
    private GameObject leftSideGO;
    private GameObject rightSideGO;
    private GameObject behindSideGO;
    private GameObject planeGO;
    private GameObject triggerGO;
    private GameObject child;

    /*void Update() {
        Debug.Log("Entri?");
        //localAgent.GetComponent<Rigidbody>().MovePosition(localAgent.transform.position + Vector3.right * Time.deltaTime);
    }

    void Start() {
        Debug.Log("Entri? 2");
    }*/

    public override void Initialize() {
        diskRB = hockeyDisk.GetComponent<Rigidbody>();

        leftSideGO = field.transform.Find("SideLeft").gameObject;
        rightSideGO = field.transform.Find("SideRight").gameObject;
        planeGO = field.transform.Find("Plane").gameObject;
        triggerGO = field.transform.Find("OnTriggerGoal").gameObject;
        child = transform.GetChild(0).gameObject;
    }

    public override void OnEpisodeBegin() {
        hockeyDisk.GetComponent<DiskBehaviour>().haveILose = false;
        hockeyDisk.transform.localPosition = createRandomPosition(false,false);
        float speed = hockeyDisk.GetComponent<DiskBehaviour>().speed;
        //speed = Random.Range(6, 20);
        hockeyDisk.GetComponent<DiskBehaviour>().speed = speed;
        Vector3 pos = createRandomPosition(true,false);
        //Debug.Log("Velocity: " + pos);
        child.transform.localPosition = createRandomPosition(false, true); //new Vector3(-0.42f,0,0);//
        diskRB.velocity = new Vector3(0f, 0f, 0f);
        diskRB.AddForce(pos.normalized * speed,
                       ForceMode.VelocityChange);
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(hockeyDisk.transform.localPosition);
        sensor.AddObservation(child.transform.localPosition);
        //Debug.Log(transform.TransformDirection(diskRB.velocity));
        //Debug.Log("AA "+diskRB.velocity);
        //Debug.Log("CC "+diskRB.transform.position);
        //Debug.Log("DD "+diskRB.transform.localPosition);
        //sensor.AddObservation(diskRB.velocity.normalized * 22);
        //sensor.AddObservation(diskRB.velocity.normalized);
        sensor.AddObservation(diskRB.velocity.normalized);
        sensor.AddObservation((int)diskRB.velocity.magnitude);
    }

    public override void OnActionReceived(float[] vectorAction) {
        Vector3 destination = new Vector3(Mathf.Clamp(vectorAction[0], -1f, 1f),
            child.transform.localPosition.y, child.transform.localPosition.z);

        //Vector3 destination = new Vector3(vectorAction[0], child.transform.localPosition.y, child.transform.localPosition.z);

        //Vector3 destination = new Vector3(Mathf.Clamp(vectorAction[0], -1.80f - child.transform.localPosition.x, +1.10f - -child.transform.localPosition.x),0,0);
        //if (!(destination.magnitude < 0.0001f)) {
        //Vector3 newPosition = child.GetComponent<Rigidbody>().position + child.transform.TransformDirection(destination);
        //Debug.Log("CC " + child.GetComponent<Rigidbody>().position);
        //Debug.Log("DD " + destination);
        child.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
        child.GetComponent<Rigidbody>().AddForce(destination.normalized * agentSpeed,
                 ForceMode.VelocityChange);
        //print(destination);
        //child.GetComponent<Rigidbody>().MovePosition(newPosition);
        //}

        if (hockeyDisk.GetComponent<DiskBehaviour>().haveILose == false) {
            SetReward(0.05f);
        } else {
            SetReward(-5f);
            EndEpisode();
        }
    }

    private Vector3 createRandomPosition(bool velocity, bool agent) {

        float rndPositionX = Random.Range(-1.53f, 1.53f);
        float rndPositionZ = Random.Range(-3.282f, 0.5f);

        if(agent) 
            return new Vector3(rndPositionX, child.transform.localPosition.y, child.transform.localPosition.z);
        else {
            if(velocity)
                rndPositionZ = Random.Range(1f, 4.5f);
            return new Vector3(rndPositionX, diskRB.transform.localPosition.y, rndPositionZ);
        }
    }

}
