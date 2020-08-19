using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleCollision2 : MonoBehaviour{

    public GameObject disk;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(disk.GetComponent<MeshCollider>(), this.gameObject.GetComponent<BoxCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
