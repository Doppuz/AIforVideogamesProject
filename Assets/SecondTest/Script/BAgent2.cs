using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BAgent2 : Agent{
    [Header("Agent parameter")]

    public GameObject hockeyDisk;
    public GameObject field;
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
    }

    public override void OnEpisodeBegin() {
        /*if (training) {
            if(behindZ > 0) 
                behindGO.transform.localPosition = new Vector3(behindGO.transform.localPosition.x, behindGO.transform.localPosition.y, Random.Range(1f,4.7f));
            else
                behindGO.transform.localPosition = new Vector3(behindGO.transform.localPosition.x, behindGO.transform.localPosition.y, Random.Range(-4.7f, -1f));
        }*/
        hockeyDisk.GetComponent<DiskBehaviour2>().haveILose = false;
        hockeyDisk.transform.localPosition = createRandomPosition(false,false);
        Debug.Log(hockeyDisk.transform.localPosition);
        float speed = hockeyDisk.GetComponent<DiskBehaviour2>().speed;
        Vector3 pos = createRandomPosition(true,false);
        child.transform.localPosition = createRandomPosition(false, true);
        diskRB.velocity = new Vector3(0f, 0f, 0f);
        diskRB.AddForce(pos.normalized * speed,
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

        /*if (training) {
            if (behindZ > 0)
                rndPositionZ = Random.Range(behindGO.transform.localPosition.z - 2, behindGO.transform.localPosition.z - 0.5f);
            else
                rndPositionZ = Random.Range(behindGO.transform.localPosition.z + 0.5f, behindGO.transform.localPosition.z + 2);
        }*/

        if (agent) 
            return new Vector3(rndPositionX, child.transform.localPosition.y, child.transform.localPosition.z);
        else {
            if (velocity) {
                float rndPositionZ1 = Random.Range(-4f, hockeyDisk.transform.localPosition.z -1f);
                float rndPositionZ2 = Random.Range(hockeyDisk.transform.localPosition.z + 1f, 4f);
                if (Random.Range(0, 2) == 0)
                    rndPositionZ = rndPositionZ1;
                else
                    rndPositionZ = rndPositionZ2;
            }
            return new Vector3(rndPositionX, diskRB.transform.localPosition.y, rndPositionZ);
        }
    }

}
