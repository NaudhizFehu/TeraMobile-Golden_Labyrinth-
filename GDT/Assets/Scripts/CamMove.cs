using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

    public Camera Cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 Pos = Cam.transform.position;
        Vector3 Rot = Vector3.zero;

        if(Input.GetKey(KeyCode.T))
        {
            Pos.z += 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.G))
        {
            Pos.z -= 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.F))
        {
            Pos.x -= 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.H))
        {
            Pos.x += 5 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Keypad8))
        {
            Rot.y = 0;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            Rot.y = 180;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            Rot.y = 90;
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            Rot.y = 270;
        }

        Cam.transform.Rotate(Rot);
        Cam.transform.position = Pos;
    }
}
