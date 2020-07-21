using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BAgent : Agent{
    [Header("Agent parameter")]

    public GameObject hockeyDisk;
    public GameObject field;

    private Rigidbody diskRB;
    private GameObject leftSideGO;
    private GameObject rightSideGO;
    private GameObject behindSideGO;
    private GameObject planeGO;
    private GameObject triggerGO;
    private GameObject localAgent;

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
        behindSideGO = field.transform.Find("SideBehind").gameObject;
        planeGO = field.transform.Find("Plane").gameObject;
        triggerGO = field.transform.Find("OnTriggerGoal").gameObject;
        localAgent = transform.Find("Character").gameObject;
        /*Debug.Log("A "+transform.localPosition);
        Debug.Log("AA " + transform.position);
        Debug.Log("AAA " + localAgent.transform.position);
        Debug.Log("AAAA " + localAgent.transform.localPosition);*/
    }

    public override void OnEpisodeBegin() {
        hockeyDisk.GetComponent<DiskBehaviour>().haveILose = false;
        hockeyDisk.transform.position = createRandomPosition(false,false);
        float speed = hockeyDisk.GetComponent<DiskBehaviour>().speed;
        speed = Random.Range(5, 25);
        hockeyDisk.GetComponent<DiskBehaviour>().speed = speed;
        diskRB.velocity = createRandomPosition(true,false).normalized * speed;
        localAgent.transform.position = createRandomPosition(false,true);
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(hockeyDisk.transform.position);
        sensor.AddObservation(localAgent.transform.position);
        sensor.AddObservation(diskRB.velocity);
    }

    public override void OnActionReceived(float[] vectorAction) {
        /*localAgent.GetComponent<Rigidbody>().MovePosition(localAgent.transform.position +
            new Vector3(vectorAction[0], localAgent.transform.position.y, localAgent.transform.position.z).normalized * 5 * Time.deltaTime);*/
        localAgent.GetComponent<Rigidbody>().velocity = new Vector3(vectorAction[0], localAgent.transform.position.y, localAgent.transform.position.z).normalized * 5;
        if (hockeyDisk.GetComponent<DiskBehaviour>().haveILose == false) {
            SetReward(0.05f);
        } else {
            SetReward(-1f);
            EndEpisode();
        }
    }

    private Vector3 createRandomPosition(bool velocity, bool agent) {

        float rndPositionX;
        float rndPositionZ;

        Vector3 sideSize = leftSideGO.GetComponent<Collider>().bounds.size;
        Vector3 halfPlaneSize = planeGO.GetComponent<Collider>().bounds.size;

        float z = behindSideGO.transform.position.z;

        if (!agent)
            rndPositionX = Random.Range(leftSideGO.transform.position.x - sideSize.x, rightSideGO.transform.position.x + sideSize.x);
        else {
            rndPositionX = Random.Range(leftSideGO.transform.position.x - sideSize.x, rightSideGO.transform.position.x + sideSize.x);
            return new Vector3(rndPositionX, localAgent.transform.position.y, localAgent.transform.position.z);
        }

        if(!velocity)
            rndPositionZ = Random.Range(z + sideSize.x, (halfPlaneSize.z) / 2 + z);
        else
            rndPositionZ = Random.Range(z + sideSize.x, (halfPlaneSize.z) + z);

        return new Vector3(rndPositionX, hockeyDisk.transform.position.y, rndPositionZ);
    }


    /*private Vector3 createRandomVelocity() {
        Vector3 sideSize = leftSideGO.GetComponent<Collider>().bounds.size;
        Vector3 halfPlaneSize = planeGO.GetComponent<Collider>().bounds.size;

        float z = behindSideGO.transform.position.z;
        float lenght = halfPlaneSize.z - (behindSideGO.transform.position.z - hockeyDisk.transform.position.z);
        float rndPositionX = Random.Range(leftSideGO.transform.position.x - sideSize.x, rightSideGO.transform.position.x + sideSize.x);
        //float rndPositionZ = Random.Range(hockeyDisk.transform.position.z + 1f, triggerGO.transform.position.z);
        //float rndPositionZ = Random.Range(behindSideGO.transform.position.z, behindSideGO.transform.position.z + halfPlaneSize.z);
        float rndPositionZ = Random.Range(z + sideSize.x, (halfPlaneSize.z) + z);
        return new Vector3(rndPositionX, hockeyDisk.transform.position.y, rndPositionZ);
    }*/
    /*
     * float x = left.transform.position.x - 1.2f;
    float y = right.transform.position.x + 1.2f;
    float z = GameObject.Find("SideBehind").transform.position.z;
    Vector3 sideSize = GameObject.Find("SideLeft").GetComponent<Collider>().bounds.size;
    Vector3 halfPlaneSize = GameObject.Find("Plane").GetComponent<Collider>().bounds.size;
    float circleRay = GetComponent<Collider>().bounds.size.x / 2;
    float trueHalfPlane = halfPlaneSize.x - 2 * sideSize.x;
    Debug.Log(trueHalfPlane + " " + (right.transform.position.x + sideSize.x) + " " + (right.transform.position.x + sideSize.x + trueHalfPlane - circleRay));
    float rndPosition = Random.Range(left.transform.position.x - sideSize.x, right.transform.position.x + sideSize.x);
    transform.position = new Vector3(rndPosition, transform.position.y, Random.Range(z + sideSize.x, (halfPlaneSize.z) / 2 + z));
    Debug.Log("AA " + GameObject.Find("Plane").GetComponent<Renderer>().bounds.size + " " + sideSize + " " + GetComponent<Collider>().bounds.size);

     */


    /*public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        Vector3 pos = transform.position;

        if(Input.GetKey("s")) {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("w")) {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("d")) {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }*/
}
