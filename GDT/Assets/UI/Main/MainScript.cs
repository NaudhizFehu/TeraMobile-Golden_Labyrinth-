using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        UIRoot root = GameObject.Find("UI Root (2D)").GetComponent<UIRoot>();

        root.automatic = true;
        root.manualHeight = 720;
        root.minimumHeight = 320;
        root.maximumHeight = 1536;

        GameObject obj = GameObject.Find("Camera");
        Camera cam = obj.GetComponent<Camera>();

        float perx = 480.0f / Screen.width;
        float pery = 320.0f / Screen.height;
        float v = (perx > pery) ? perx : pery;
        cam.orthographicSize = v;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
