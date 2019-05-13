using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardScript : MonoBehaviour {

    enum PlayerState
    {
        Wait = 0,
        Combo1Start,
        Combo1Shot,
        Combo1End,
        Combo2Start,
        Combo2Shot,
        Combo2End,
        Combo3,
        Combo4Start,
        Combo4Shot,
        Combo4End,
        EvadeStart,
        EvadeShot,
        EvadeEnd,
        WeakPointStart,
        WeakPointShot,
        WeakPointEnd,
        HellicopterCutStart,
        HellicopterCutShot1,
        HellicopterCutShot2,
        HellicopterCutShot3,
        HellicopterCutShot3End,
        ThrowGlaveStart,
        ThrowGlaveJump,
        ThrowGlaveShotStart,
        ThrowGlaveShot,
        ThrowGlaveShotEnd,
        BodyCrescentCut1Start,
        BodyCrescentCut1Shot,
        BodyCrescentCut1End,
        BodyCrescentCut1R,
        BodyCrescentCut2Start,
        BodyCrescentCut2Shot,
        BodyCrescentCut2End,
        BodyCrescentCut2R,
        Run
    }



    public GameObject PosObj;

    public float Speed;
    public GameObject BodyObj;
    public GameObject FaceObj;
    public GameObject HairObj;

    private float rotationSpeed = 360f;
    private float runSpeed = 15f;
    private bool isAttack = false;

    private Animator BodyAnimator;
    private Animator FaceAnimator;
    private Animator HairAnimator;

    private PlayerState state;
    public AudioClip[] AudioList;
    private AudioSource audioSource;

    public bool isEvade { get; set; }
    public bool isSkill1 { get; set; }
    public bool isSkill2 { get; set; }
    public bool isSkill3 { get; set; }
    public bool isSkill4 { get; set; }
    public bool isSkill5 { get; set; }
    public bool isSkill6 { get; set; }

    // Use this for initialization
    void Start () {
        BodyAnimator = BodyObj.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = PosObj.transform.position;
        
        
         RotForward();

	}

    void ShotSound()
    {
        audioSource.clip = AudioList[Random.Range(0, 6)];
        audioSource.Play();
    }
    void LongSound()
    {
        audioSource.clip = AudioList[Random.Range(6, 12)];
        audioSource.Play();
    }

    public void RotForward()
    {
        Vector3 VecTemp = Vector3.zero;
        if(Input.GetKey(KeyCode.H))
        {
            VecTemp.y += 1;
        }
        if (Input.GetKey(KeyCode.K))
        {
            VecTemp.y -= 1;
        }

        this.gameObject.transform.Rotate(VecTemp);
    }

    IEnumerator PlayCombo1()
    {
        state = PlayerState.Combo1Start;

        isAttack = true;


        yield return new WaitForSeconds(0.27f);

        //if (state == PlayerState.Combo1Start && isAttack)
        //{
        //    if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo1Start"))
        //        if (BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        //        {
        state = PlayerState.Combo1Shot;
        ShotSound();

        yield return new WaitForSeconds(0.1f);
        //        }
        //}

        yield return new WaitForSeconds(0.17f);

        state = PlayerState.Combo1End;
    }

    IEnumerator PlayCombo2()
    {
        state = PlayerState.Combo2Start;

        isAttack = true;

        yield return new WaitForSeconds(0.27f);

        state = PlayerState.Combo2Shot;
        ShotSound();

        yield return new WaitForSeconds(0.1f);


        yield return new WaitForSeconds(0.17f);

        state = PlayerState.Combo2End;
    }

    IEnumerator PlayCombo3()
    {
        state = PlayerState.Combo3;
        LongSound();
        isAttack = true;

        yield return new WaitForSeconds(0.5f);


        yield return new WaitForSeconds(0.6f);

    }

    IEnumerator PlayCombo4()
    {
        state = PlayerState.Combo4Start;

        isAttack = true;

        yield return new WaitForSeconds(0.5f);

        if (state == PlayerState.Combo4Start && isAttack)
        {
            if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo4Start"))
                if (BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    state = PlayerState.Combo4Shot;
                }
        }
        LongSound();

        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(0.25f);

        if (state == PlayerState.Combo4Shot && isAttack)
        {
            if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo4Shot"))
                if (BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    state = PlayerState.Combo4End;
                }
        }

        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);

         
        state = PlayerState.Wait;

        isAttack = false;

    }

    public void Skill1()
    {
        if (isEvade) return;

        isSkill1 = true;

        if (!isAttack && state == PlayerState.Wait)
        {
            StartCoroutine(this.PlayCombo1());
        }
        else if (isAttack && state == PlayerState.Combo1End)
        {
            if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo1End"))
            //if(BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
            {
                StartCoroutine(this.PlayCombo2());
            }
        }
        else if (isAttack && state == PlayerState.Combo2End)
        {
            if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo2End"))
            //if (BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
            {
                StartCoroutine(this.PlayCombo3());
            }
        }
        else if (isAttack && state == PlayerState.Combo3)
        {
            if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo3"))
            {
                if (BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    StartCoroutine(this.PlayCombo4());
                }
            }
        }
    }

    public void Evade()
    {
        isEvade = true;
        isSkill2 = true;
        StopAllCoroutines();
        StartCoroutine(EvadePlay());
    }

    IEnumerator EvadePlay()
    {
        yield return new WaitForSeconds(0.01f);

        isAttack = false;

        state = PlayerState.EvadeStart;
        LongSound();

        yield return new WaitForSeconds(0.25f);

         
        state = PlayerState.EvadeShot;

        yield return new WaitForSeconds(0.5f);

         
        state = PlayerState.EvadeEnd;

        yield return new WaitForSeconds(0.4f);

        isEvade = false;
        isSkill2 = false;
         
        state = PlayerState.Wait;
    }

    public void WeakPoint()
    {
        isSkill3 = true;
        StartCoroutine(WeakPointPlay());
    }

    IEnumerator WeakPointPlay()
    {
        isAttack = true;
        LongSound();
        state = PlayerState.WeakPointStart;

        yield return new WaitForSeconds(1f);

        // 
        state = PlayerState.WeakPointShot;

        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.8f);

         
        state = PlayerState.WeakPointEnd;
        yield return new WaitForSeconds(1f);

         
        state = PlayerState.Wait;

        isAttack = false;
        isSkill3 = false;
    }

    public void HCC()
    {
        isSkill4 = true;
        StartCoroutine(HCCPlay());
    }

    IEnumerator HCCPlay()
    {
        isAttack = true;

        state = PlayerState.HellicopterCutStart;

        yield return new WaitForSeconds(0.27f);

         
        state = PlayerState.HellicopterCutShot1;

        yield return new WaitForSeconds(0.25f);

         
        state = PlayerState.HellicopterCutShot2;

        yield return new WaitForSeconds(0.2f);

        state = PlayerState.HellicopterCutStart;

        yield return new WaitForSeconds(0.25f);

        state = PlayerState.HellicopterCutShot1;

        yield return new WaitForSeconds(0.25f);
         
        state = PlayerState.HellicopterCutShot2;

        yield return new WaitForSeconds(0.2f);

        state = PlayerState.HellicopterCutShot3;
        LongSound();

        yield return new WaitForSeconds(0.85f);
        yield return new WaitForSeconds(0.15f);

         
        state = PlayerState.HellicopterCutShot3End;

        yield return new WaitForSeconds(0.5f);
         
        state = PlayerState.Wait;

        isAttack = false;
        isSkill4 = false;
    }

    public void ThrowGlave()
    {
        isSkill5 = true;
        StartCoroutine(ThrowGlavePlay());
    }

    IEnumerator ThrowGlavePlay()
    {
        isAttack = true;

        state = PlayerState.ThrowGlaveStart;
        ShotSound();

        yield return new WaitForSeconds(0.5f);
         
        state = PlayerState.ThrowGlaveJump;

        yield return new WaitForSeconds(0.5f);

         
        state = PlayerState.ThrowGlaveShotStart;
        yield return new WaitForSeconds(0.25f);


        yield return new WaitForSeconds(0.25f);
         
        state = PlayerState.ThrowGlaveShot;
        ShotSound();

        yield return new WaitForSeconds(0.5f);
         
        state = PlayerState.ThrowGlaveShotEnd;

        yield return new WaitForSeconds(0.5f);
         
        state = PlayerState.Wait;

        isAttack = false;
        isSkill5 = false;
    }

    public void CrescentCut()
    {
        isSkill6 = true;
        StartCoroutine(CrescentCutPlay());
    }

    IEnumerator CrescentCutPlay()
    {
        isAttack = true;

        state = PlayerState.BodyCrescentCut1Start;

        yield return new WaitForSeconds(0.5f);

         
        state = PlayerState.BodyCrescentCut1Shot;
        LongSound();

        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.4f);

        state = PlayerState.BodyCrescentCut1End;

        yield return new WaitForSeconds(0);

         
        state = PlayerState.BodyCrescentCut1R;

        yield return new WaitForSeconds(0.5f);

        state = PlayerState.BodyCrescentCut2Start;

        yield return new WaitForSeconds(1.3f);

         
        state = PlayerState.BodyCrescentCut2Shot;
        ShotSound();

        yield return new WaitForSeconds(0.5f);

        state = PlayerState.BodyCrescentCut2End;

        yield return new WaitForSeconds(0.5f);

        state = PlayerState.BodyCrescentCut2R;

        yield return new WaitForSeconds(0.5f);

        state = PlayerState.Wait;

        isAttack = false;
        isSkill6 = false;
    }
}

