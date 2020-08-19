using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BAgent : Agent{
    [Header("Agent parameter")]

    public GameObject hockeyDisk;

    public float agentSpeed;

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
        child = transform.GetChild(0).gameObject;
    }

    public override void OnEpisodeBegin() {
        hockeyDisk.GetComponent<DiskBehaviour>().haveILose = false;
        hockeyDisk.transform.localPosition = createRandomPosition(false,false);
        float speed = hockeyDisk.GetComponent<DiskBehaviour>().speed;
        child.transform.localPosition = createRandomPosition(false, true);
        Vector3 pos = createRandomPosition(true,false);
        diskRB.velocity = new Vector3(0f, 0f, 0f);
        print(speed);
        diskRB.AddForce(pos.normalized * speed,
                       ForceMode.VelocityChange);
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(hockeyDisk.transform.localPosition);
        sensor.AddObservation(child.transform.localPosition.x);
        sensor.AddObservation(diskRB.velocity);
    }

    public override void OnActionReceived(float[] vectorAction) {
        Vector3 destination = new Vector3(Mathf.Clamp(vectorAction[0], -1f, 1f),
            child.transform.localPosition.y, child.transform.localPosition.z);

        child.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
        child.GetComponent<Rigidbody>().AddForce(destination.normalized * agentSpeed,
                 ForceMode.VelocityChange);


        if (hockeyDisk.GetComponent<DiskBehaviour>().haveILose == false) {
            SetReward(0.05f);
        } else {
            SetReward(-5f);
            EndEpisode();
        }
    }

    private Vector3 createRandomPosition(bool velocity, bool agent) {

        float rndPositionX = Random.Range(-1.53f, 1.53f);
        float rndPositionZ = Random.Range(-0.5f, 0.5f);

        if(agent) 
            return new Vector3(rndPositionX, child.transform.localPosition.y, child.transform.localPosition.z);
        else {
            if (velocity) {
                /*float rndPositionZ1 = Random.Range(-4f, child.transform.localPosition.z - 2f);
                float rndPositionZ2 = Random.Range(child.transform.localPosition.z  + 2f, 4f);
                if (Random.Range(0, 2) == 0)
                    rndPositionZ = rndPositionZ1;
                else
                    rndPositionZ = rndPositionZ2;*/

                return new Vector3(Random.Range(-1f, 1f), diskRB.transform.localPosition.y, Random.Range(-1f, 1f));
            }
            return new Vector3(rndPositionX, diskRB.transform.localPosition.y, rndPositionZ);
        }
    }

}
