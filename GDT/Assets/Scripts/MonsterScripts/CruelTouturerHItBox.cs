﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruelTouturerHItBox : MonoBehaviour {

    public GameObject Monster;
    Animator ani;
    public GameObject Player;
    Animator Pani;

	// Use this for initialization
	void Start () {

        ani = Monster.GetComponent<Animator>();
        Pani = Player.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (Pani.GetCurrentAnimatorStateInfo(0).IsName("BodyEvadeStart") &&
            Pani.GetCurrentAnimatorStateInfo(0).IsName("BodyEvadeShot") &&
            Pani.GetCurrentAnimatorStateInfo(0).IsName("BodyEvadeEnd")) return;

        if(other.tag == "Player")
        {
            //Debug.Log("Monster : " + other.gameObject.name);
            if(ani.GetCurrentAnimatorStateInfo(0).IsName("Atk01"))
            {
                if(ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5 &&
                   ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.515)
                {
                    other.SendMessage("HPMinus", Random.Range(200, 751));
                }
            }
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Atk04"))
            {
                if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5 &&
                   ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.515)
                {
                    other.SendMessage("HPMinus", Random.Range(200, 751));
                }
            }
        }
    }
}
