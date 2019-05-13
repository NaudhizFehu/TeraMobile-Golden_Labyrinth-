using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaidunsElteWarriorSphere : MonoBehaviour {

    public KaidunsElteWarriorControl KaidunElteWarrior;

    float NoticePlayerTime = 1f;
    float NoticePlayerCurTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        KaidunElteWarrior.animator.SetBool("isSearchPlayer", KaidunElteWarrior.isFindPlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (KaidunElteWarrior.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                NoticePlayerCurTime += Time.deltaTime;
                KaidunElteWarrior.Target = other.gameObject;
                KaidunElteWarrior.gameObject.transform.LookAt(KaidunElteWarrior.Target.transform);


                if (NoticePlayerTime < NoticePlayerCurTime)
                {
                    KaidunElteWarrior.isFindPlayer = true;
                    KaidunElteWarrior.animator.SetBool("isSearchPlayer", KaidunElteWarrior.isFindPlayer);
                    Debug.Log("인식됨");
                }
                //this.gameObject.transform.LookAt(other.gameObject.transform);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (KaidunElteWarrior.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                KaidunElteWarrior.Target = null;
                KaidunElteWarrior.gameObject.transform.rotation = KaidunElteWarrior.SaveRot;
            }
        }
    }
}
