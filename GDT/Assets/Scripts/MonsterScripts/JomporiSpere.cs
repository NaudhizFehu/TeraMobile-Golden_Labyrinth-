using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JomporiSpere : MonoBehaviour {

    public JomporiControl Jompori;

    float NoticePlayerTime = 0.3f;
    float NoticePlayerCurTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        //Jompori.animator.SetBool("isSearchPlayer", Jompori.isFindPlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (Jompori.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                NoticePlayerCurTime += Time.deltaTime;
                Jompori.Target = other.gameObject;
                Jompori.gameObject.transform.LookAt(Jompori.Target.transform);


                if (NoticePlayerTime < NoticePlayerCurTime)
                {
                    Jompori.isFindPlayer = true;
                    Jompori.animator.SetBool("isSearchPlayer", Jompori.isFindPlayer);
                    Debug.Log("인식됨");
                }
                //this.gameObject.transform.LookAt(other.gameObject.transform);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Jompori.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                Jompori.Target = null;
                Jompori.gameObject.transform.rotation = Jompori.SaveRot;
            }
        }
    }
}
