using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BAgent2 : Agent{
    [Header("Agent parameter")]

    public GameObject hockeyDisk;
    public GameObject field;
    public GameObject opponent;

    public float agentSpeed = 7f;
    public bool training = true;

    private Rigidbody diskRB;
    private GameObject behindGO;
    private GameObject child;
    private float behindZ;

    /*void Update() {
        Debug.Log("Entri?");
        //localAgent.GetComponent<Rigidbody>().MovePosition(localAgent.transform.position + Vector3.right * Time.deltaTime);
    }

    void Start() {
        Debug.Log("Entri? 2");
    }*/

    public override void Initialize() {
        diskRB = hockeyDisk.GetComponent<Rigidbody>();

        child = transform.GetChild(0).gameObject;

        behindZ = opponent.transform.localPosition.z;
    }

    public override void OnEpisodeBegin() {
        if (training) {
            if(behindZ > 0)
                opponent.transform.localPosition = new Vector3(opponent.transform.localPosition.x, opponent.transform.localPosition.y, Random.Range(1.5f,4.7f));
            else
                opponent.transform.localPosition = new Vector3(opponent.transform.localPosition.x, opponent.transform.localPosition.y, Random.Range(-4.7f, -1.5f));
        }
        hockeyDisk.GetComponent<DiskBehaviour2>().haveILose = false;
        hockeyDisk.transform.localPosition = createRandomPosition(false,false);
        float speed = hockeyDisk.GetComponent<DiskBehaviour2>().speed;
        Vector3 pos = createRandomPosition(true,false);
        child.transform.localPosition = createRandomPosition(false, true);
        diskRB.velocity = new Vector3(0f, 0f, 0f);
        diskRB.AddForce(Vector3.forward * speed,
                       ForceMode.VelocityChange);
        
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(hockeyDisk.transform.localPosition);
        sensor.AddObservation(child.transform.localPosition.x);
        sensor.AddObservation(child.transform.localPosition.z);
        sensor.AddObservation(diskRB.velocity);
        //sensor.AddObservation(opponent.transform.localPosition.z);
    }

    public override void OnActionReceived(float[] vectorAction) {
        Vector3 destination = new Vector3(Mathf.Clamp(vectorAction[0], -1f, 1f),
            child.transform.localPosition.y, Mathf.Clamp(vectorAction[1], -1f, 1f));

        child.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
        child.GetComponent<Rigidbody>().AddForce(destination.normalized * agentSpeed,
                 ForceMode.VelocityChange);

        if (hockeyDisk.GetComponent<DiskBehaviour2>().haveILose == false) {
            SetReward(0.05f);
        } else {
            SetReward(-5f);
            EndEpisode();
        }
    }

    private Vector3 createRandomPosition(bool velocity, bool agent) {


        float rndPositionZ = Random.Range(-0.5f, 0.5f);
        float rndPositionX = Random.Range(-1.53f, 1.53f);

        if (training) {
            if (behindZ > 0)
                rndPositionZ = Random.Range(opponent.transform.localPosition.z - 2, opponent.transform.localPosition.z - 0.5f);
            else
                rndPositionZ = Random.Range(opponent.transform.localPosition.z + 0.5f, opponent.transform.localPosition.z + 2);
        }

        if (agent) 
            return new Vector3(rndPositionX, child.transform.localPosition.y, child.transform.localPosition.z);
        else {
            if (velocity) {
                float rndPositionZ1 = Random.Range(-5f, opponent.transform.localPosition.z -2f);
                float rndPositionZ2 = Random.Range(opponent.transform.localPosition.z + 2f, 5f);
                if (Random.Range(0, 2) == 0)
                    rndPositionZ = rndPositionZ1;
                else
                    rndPositionZ = rndPositionZ2;
            }
            return new Vector3(rndPositionX, diskRB.transform.localPosition.y, rndPositionZ);
        }
    }

}
