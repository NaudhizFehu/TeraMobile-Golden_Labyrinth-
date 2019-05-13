using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2DoorScript : MonoBehaviour {

    enum DoorState
    {
        Closed = 0,
        Open,
        Closing,
        Opening
    }

    private Animator R2DoorAni;

	// Use this for initialization
	void Start () {
        R2DoorAni = GetComponent<Animator>();
        R2DoorAni.SetInteger("State", 3);
        Debug.Log(DoorState.Open.ToString());
	}
	
	// Update is called once per frame
	void Update () {

       

        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            R2DoorAni.SetInteger("State", 3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            R2DoorAni.SetInteger("State", 2);
        }
    }

    IEnumerator OpenDoor()
    {
        R2DoorAni.SetInteger("State", 3);

        yield return new WaitForSeconds(1f);

        R2DoorAni.SetInteger("State", 1);
    }

    IEnumerator CloseDoor()
    {
        R2DoorAni.SetInteger("State", 2);

        yield return new WaitForSeconds(1f);

        R2DoorAni.SetInteger("State", 0);
    }
}
