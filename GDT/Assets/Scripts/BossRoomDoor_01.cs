using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomDoor_01 : MonoBehaviour {

    private float DoorSwapStateTime = 0.0f;
    private Animator DoorAni;
    int StateValue;

	// Use this for initialization
	void Start () {
        DoorAni = GetComponent<Animator>();
        StateValue = 3;
        DoorAni.SetInteger("State", StateValue);

    }
	
	// Update is called once per frame
	void Update () {

        DoorSwapStateTime += Time.deltaTime;

        if(DoorSwapStateTime > 10.0f)
        {
            if(StateValue == 3)
            {
                DoorSwapStateTime = 0.0f;
                StateValue = 2;
                DoorAni.SetInteger("State", StateValue);
            }
            else if(StateValue == 2)
            {
                DoorSwapStateTime = 0.0f;
                StateValue = 3;
                DoorAni.SetInteger("State", StateValue);
            }
        }
	}
}
