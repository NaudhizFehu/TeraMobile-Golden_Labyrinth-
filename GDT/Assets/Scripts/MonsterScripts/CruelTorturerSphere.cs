using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruelTorturerSphere : MonoBehaviour {

    public CruelTorturerControl CruelTorturer;

    float NoticePlayerTime = 1f;
    float NoticePlayerCurTime = 0.0f;

    // Use this for initialization
    void Start () {
        //CruelTorturer.animator.SetBool("isSearchPlayer", CruelTorturer.isFindPlayer);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (CruelTorturer.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                NoticePlayerCurTime += Time.deltaTime;
                CruelTorturer.Target = other.gameObject;
                CruelTorturer.gameObject.transform.LookAt(CruelTorturer.Target.transform);


                if (NoticePlayerTime < NoticePlayerCurTime)
                {
                    CruelTorturer.isFindPlayer = true;
                    CruelTorturer.animator.SetBool("isSearchPlayer", CruelTorturer.isFindPlayer);
                    Debug.Log("인식됨");
                }
                //this.gameObject.transform.LookAt(other.gameObject.transform);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (CruelTorturer.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                CruelTorturer.Target = null;
                CruelTorturer.gameObject.transform.rotation = CruelTorturer.SaveRot;
            }
        }
    }
}
