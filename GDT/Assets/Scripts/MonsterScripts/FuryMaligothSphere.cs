using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryMaligothSphere : MonoBehaviour {

    public FuryMaligothControl FuryMaligoth;

    float NoticePlayerTime = 1f;
    float NoticePlayerCurTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        FuryMaligoth.animator.SetBool("isSearchPlayer", FuryMaligoth.isFindPlayer);
    }

    private void OnTriggerStay(Collider other)
    {
        if (FuryMaligoth.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                NoticePlayerCurTime += Time.deltaTime;
                FuryMaligoth.Target = other.gameObject;
                FuryMaligoth.gameObject.transform.LookAt(FuryMaligoth.Target.transform);


                if (NoticePlayerTime < NoticePlayerCurTime)
                {
                    FuryMaligoth.isFindPlayer = true;
                    FuryMaligoth.animator.SetBool("isSearchPlayer", FuryMaligoth.isFindPlayer);
                    Debug.Log("인식됨");
                }
                //this.gameObject.transform.LookAt(other.gameObject.transform);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (FuryMaligoth.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                FuryMaligoth.Target = null;
                FuryMaligoth.gameObject.transform.rotation = FuryMaligoth.SaveRot;
            }
        }
    }
}
