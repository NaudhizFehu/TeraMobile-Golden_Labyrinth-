using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpServantSphere : MonoBehaviour {

    public ImpServantControl ImpServant;

    float NoticePlayerTime = 0.3f;
    float NoticePlayerCurTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        //ImpServant.animator.SetBool("isSearchPlayer", ImpServant.isFindPlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (ImpServant.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                NoticePlayerCurTime += Time.deltaTime;
                ImpServant.Target = other.gameObject;
                ImpServant.gameObject.transform.LookAt(ImpServant.Target.transform);


                if (NoticePlayerTime < NoticePlayerCurTime)
                {
                    ImpServant.isFindPlayer = true;
                    ImpServant.animator.SetBool("isSearchPlayer", ImpServant.isFindPlayer);
                    Debug.Log("인식됨");
                }
                //this.gameObject.transform.LookAt(other.gameObject.transform);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (ImpServant.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                ImpServant.Target = null;
                ImpServant.gameObject.transform.rotation = ImpServant.SaveRot;
            }
        }
    }
}
