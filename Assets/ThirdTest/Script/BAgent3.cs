using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BAgent3 : Agent{
    [Header("Agent parameter")]

    public GameObject hockeyDisk;
    public GameObject opponent;

    public float agentSpeed = 7f;
    public bool training = true;

    private Rigidbody diskRB;
    private GameObject behindGO;
    private GameObject child;
    private float sideBehindInitialPos;
    private float agentInitialPos;

    public override void Initialize() {
        diskRB = hockeyDisk.GetComponent<Rigidbody>();

        child = transform.GetChild(0).gameObject;

        sideBehindInitialPos = opponent.transform.localPosition.z;

        agentInitialPos = transform.localPosition.z;
    }

    public override void OnEpisodeBegin() {
        if (training) {
            if(sideBehindInitialPos > 0)
                opponent.transform.localPosition = new Vector3(opponent.transform.localPosition.x, opponent.transform.localPosition.y, Random.Range(4f, 8.5f));
            else
                opponent.transform.localPosition = new Vector3(opponent.transform.localPosition.x, opponent.transform.localPosition.y, Random.Range(-8.5f, -4f));
        }
        hockeyDisk.GetComponent<DiskBehaviour3>().haveILose = false;
        hockeyDisk.transform.localPosition = createRandomPosition(false,false);
        float speed = hockeyDisk.GetComponent<DiskBehaviour3>().speed;
        Vector3 pos = createRandomPosition(true,false);
        child.transform.localPosition = createRandomPosition(false, true);
        child.transform.localPosition = new Vector3(0, 0, -1.65f);
        diskRB.velocity = new Vector3(0f, 0f, 0f);
        diskRB.AddForce(Vector3.forward * speed,
                       ForceMode.VelocityChange);
        
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(hockeyDisk.transform.localPosition);
        sensor.AddObservation(child.transform.localPosition.x);
        sensor.AddObservation(child.transform.localPosition.z);
        sensor.AddObservation(diskRB.velocity);
    }

    public override void OnActionReceived(float[] vectorAction) {
        Vector3 destination = new Vector3(Mathf.Clamp(vectorAction[0], -1f, 1f),
            child.transform.localPosition.y, Mathf.Clamp(vectorAction[1], -1f, 1f));

        child.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
        child.GetComponent<Rigidbody>().AddForce(destination.normalized * agentSpeed,
                 ForceMode.VelocityChange);

        if (hockeyDisk.GetComponent<DiskBehaviour3>().haveILose == false) {
            SetReward(0.05f);
        } else {
            SetReward(-5f);
            EndEpisode();
        }
    }

    private Vector3 createRandomPosition(bool velocity, bool agent) {


        float rndPositionZ = Random.Range(-0.5f, 0.5f);
        float rndPositionX = Random.Range(-1.3f, 1.3f);

        if (training) {
            if (sideBehindInitialPos > 0)
                rndPositionZ = Random.Range(opponent.transform.localPosition.z - 2, opponent.transform.localPosition.z - 0.5f);
            else
                rndPositionZ = Random.Range(opponent.transform.localPosition.z + 0.5f, opponent.transform.localPosition.z + 2);
        }

        if (agent) {
            if(agentInitialPos > 0)
                return new Vector3(rndPositionX, child.transform.localPosition.y, Random.Range(-3.2f,-1.2f));
            else
                return new Vector3(rndPositionX, child.transform.localPosition.y, Random.Range(-13.2f, -11.2f));
        } else {
            if (velocity) {
                return new Vector3(Random.Range(-1f, 1f), diskRB.transform.localPosition.y, Random.Range(-1f, 1f));
            }
            return new Vector3(rndPositionX, diskRB.transform.localPosition.y, rndPositionZ);
        }
    }

}
