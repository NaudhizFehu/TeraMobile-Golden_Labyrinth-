using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalKumasSphere : MonoBehaviour {

    public ElementalKumasControl ElementalKumas;

    float NoticePlayerTime = 1f;
    float NoticePlayerCurTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        //ElementalKumas.animator.SetBool("isSearchPlayer", ElementalKumas.isFindPlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (ElementalKumas.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                NoticePlayerCurTime += Time.deltaTime;
                ElementalKumas.Target = other.gameObject;
                ElementalKumas.gameObject.transform.LookAt(ElementalKumas.Target.transform);


                if (NoticePlayerTime < NoticePlayerCurTime)
                {
                    ElementalKumas.isFindPlayer = true;
                    ElementalKumas.animator.SetBool("isSearchPlayer", ElementalKumas.isFindPlayer);
                    Debug.Log("인식됨");
                }
                //this.gameObject.transform.LookAt(other.gameObject.transform);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (ElementalKumas.isFindPlayer == false)
        {
            if (other.tag == "Player")
            {
                ElementalKumas.Target = null;
                ElementalKumas.gameObject.transform.rotation = ElementalKumas.SaveRot;
            }
        }
    }
}
