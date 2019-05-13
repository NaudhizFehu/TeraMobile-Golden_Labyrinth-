using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpServantHitBox : MonoBehaviour {

    public GameObject Monster;
    Animator ani;
    public GameObject Player;
    Animator Pani;

    private float CurTime = 0;
    private float MaxTime = 0.05f;

    // Use this for initialization
    void Start()
    {

        ani = Monster.GetComponent<Animator>();
        Pani = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (Pani.GetCurrentAnimatorStateInfo(0).IsName("BodyEvadeStart") &&
            Pani.GetCurrentAnimatorStateInfo(0).IsName("BodyEvadeShot") &&
            Pani.GetCurrentAnimatorStateInfo(0).IsName("BodyEvadeEnd")) return;

        if (other.tag == "Player")
        {
            //Debug.Log("Monster : " + other.gameObject.name);
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Atk03"))
            {
                CurTime += Time.deltaTime;
                if(CurTime >= MaxTime)
                {
                    CurTime = 0.0f;
                    other.SendMessage("HPMinus", Random.Range(100, 301));
                }
            }
        }
    }
}
