using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointMeshScript : MonoBehaviour {

    public GameObject WeakPointMesh;
    public GameObject ResetPoint;

    float ResetLength;
    float RandomSpeed;

    Vector3 RandPos;

	// Use this for initialization
	void Start () {
        ResetLength = Random.Range(1, 3.5f);
        RandomSpeed = Random.Range(5, 11);

        RandPos = this.transform.position;
        RandPos.x += Random.Range(-1.1f, 1.1f);
        RandPos.y += Random.Range(-1.1f, 1.1f);
        RandPos.z += Random.Range(0.2f, 0.3f);

        ResetPoint.transform.position = RandPos;

    }
	
	// Update is called once per frame
	void Update () {


        if ((ResetPoint.transform.position - WeakPointMesh.transform.position).magnitude <= ResetLength)
            WeakPointMesh.transform.position += WeakPointMesh.transform.forward * RandomSpeed * Time.deltaTime;
        else
        {
            
            ResetLength = Random.Range(2, 3.5f);
            RandomSpeed = Random.Range(5, 11);

            WeakPointMesh.transform.position = ResetPoint.transform.position;
        }
	}
}
