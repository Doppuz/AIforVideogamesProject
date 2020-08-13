using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleCollision : MonoBehaviour{

    public GameObject disk;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(disk.GetComponent<SphereCollider>(), this.gameObject.GetComponent<BoxCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
