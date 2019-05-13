using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadNick : MonoBehaviour {

    public Text NickText;

	// Use this for initialization
	void Start () {
		foreach(_e x in NickSaveScript.e)
        {
            NickText.text = "LV.50 " + x._s;  
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
