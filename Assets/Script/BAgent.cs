using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BAgent : Agent{
    [Header("Agent parameter")]

    public GameObject hockeyDisk;
    public GameObject field;
    public float agentSpeed = 25f;

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
        diskRB.velocity = pos.normalized * speed;
        child.transform.localPosition = createRandomPosition(false, true); //new Vector3(-0.42f,0,0);//
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(hockeyDisk.transform.localPosition);
        sensor.AddObservation(child.transform.localPosition);
        sensor.AddObservation(diskRB.transform.InverseTransformDirection(diskRB.velocity));
        //ensor.AddObservation(diskRB.velocity);
    }

    public override void OnActionReceived(float[] vectorAction) {
        //Vector3 destination = new Vector3(Mathf.Clamp(vectorAction[0], -1.88f - child.transform.localPosition.x, 1.18f - child.transform.localPosition.x),
        //    0,0);

        Vector3 destination = new Vector3(Mathf.Clamp(vectorAction[0], -0.1f, +0.1f),
            0,0);
        if (!(destination.magnitude < 0.0001f)) {
            Debug.Log(child.GetComponent<Rigidbody>().position + " " + transform.TransformDirection(destination));
            Vector3 newPosition = child.GetComponent<Rigidbody>().position + transform.TransformDirection(destination);
            child.GetComponent<Rigidbody>().MovePosition(newPosition);
        }

        if (hockeyDisk.GetComponent<DiskBehaviour>().haveILose == false) {
            SetReward(0.05f);
        } else {
            SetReward(-5f);
            EndEpisode();
        }
    }

    private Vector3 createRandomPosition(bool velocity, bool agent) {

        float rndPositionX = Random.Range(-1.53f, 1.53f);
        float rndPositionZ = Random.Range(0.5f, -3.282f);

        if(agent) 
            return new Vector3(rndPositionX, child.transform.localPosition.y, child.transform.localPosition.z);
        else {
            if(velocity)
                rndPositionZ = Random.Range(-3.82f, 4.5f);
            return new Vector3(rndPositionX, diskRB.transform.localPosition.y, rndPositionZ);
        }
    }
}
