using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxScript : MonoBehaviour {

    SphereCollider HitBox;
    public GameObject BodyAniObj;

    float DamageTick = 0.1f;
    float CurDamageTick = 0.0f;

    Animator ani;

	// Use this for initialization
	void Start () {

        HitBox = GetComponent<SphereCollider>();
        ani = BodyAniObj.GetComponent<Animator>();

        //HitBox.gameObject.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("BodyCrescentCut2Shot") ||
            ani.GetCurrentAnimatorStateInfo(0).IsName("BodyCrescentCut1Shot"))
        {
            HitBox.radius = 2.0f;
        }
        else HitBox.radius = 0.75f;

    }

    private void OnTriggerStay(Collider other)
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo1Shot") ||
            ani.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo2Shot"))
        {
            if (other.tag == "Monster")
                other.SendMessage("HPMinus", Random.Range(10000, 15001));
        }
        if(ani.GetCurrentAnimatorStateInfo(0).IsName("BodyWeakPointShot") ||
             ani.GetCurrentAnimatorStateInfo(0).IsName("BodyConbo3") ||
             ani.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo4Shot") ||
             ani.GetCurrentAnimatorStateInfo(0).IsName("BodyHCCShot2") ||
             ani.GetCurrentAnimatorStateInfo(0).IsName("BodyHCCShot3") ||
             ani.GetCurrentAnimatorStateInfo(0).IsName("BodyThrowGlaveShot"))
        {
            if (other.tag == "Monster")
            {
                CurDamageTick += Time.deltaTime;
                if (CurDamageTick >= DamageTick)
                {
                    other.SendMessage("HPMinus", Random.Range(5000, 15001));
                    CurDamageTick = 0.0f;
                }
            }
        }

        if(ani.GetCurrentAnimatorStateInfo(0).IsName("BodyCrescentCut1Shot"))
        {
            if(other.tag == "Monster")
            {
                if(ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5 &&
                    ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.55)
                {
                    other.SendMessage("HPMinus", Random.Range(200000, 300001));
                }
            }
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("BodyCrescentCut2Shot"))
        {
            if (other.tag == "Monster")
            {
                if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.75 &&
                    ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.85)
                {
                    other.SendMessage("HPMinus", Random.Range(350000, 500001));
                }
            }
        }
    }
}
