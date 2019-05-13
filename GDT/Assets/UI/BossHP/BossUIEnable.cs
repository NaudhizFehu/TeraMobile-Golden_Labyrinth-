using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIEnable : MonoBehaviour {

    public GameObject BossUI;
    //public Image BossHP;
    Renderer UIRender;

	// Use this for initialization
	void Start () {

        BossUI.SetActive(false);
        //UIRender = BossUI.GetComponentInChildren<Renderer>();
        //UIRender.enabled = false; 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BossUI.SetActive(true);
            //UIRender.enabled = true;
        }
    }
}
