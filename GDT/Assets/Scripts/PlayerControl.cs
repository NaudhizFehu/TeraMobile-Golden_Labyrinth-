using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

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

    public AudioClip[] AudioList;
    private AudioSource audioSource;

    public float Speed;
    public GameObject BodyObj;
    public GameObject FaceObj;
    public GameObject HairObj;
    public GameObject RootBone;

    private float rotationSpeed = 360f;
    private float runSpeed = 15f;
    private bool isAttack = false;

    private CharacterController pcController;
    private Animator BodyAnimator;
    private Animator FaceAnimator;
    private Animator HairAnimator;

    private PlayerState state;
    // public string TempS;
    public Joystick joystick;
    
    public bool isEvade { get; set; }
    public bool isSkill1 { get; set; }
    public bool isSkill2 { get; set; }
    public bool isSkill3 { get; set; }
    public bool isSkill4 { get; set; }
    public bool isSkill5 { get; set; }
    public bool isSkill6 { get; set; }

    public GameObject Skill1_1Eff;
    public GameObject Skill1_2Eff;
    public GameObject Skill1_3Eff;
    public GameObject Skill1_4Eff;
    public GameObject WeakPointEff;
    public GameObject Helli01Eff;
    public GameObject Helli02Eff;
    public GameObject Helli03Eff;
    public GameObject GlaveShot01;
    public GameObject GroundEff;
    public GameObject Cresent01Eff;
    public GameObject Cresent02Eff;

    public float CurHP;
    public float MaxHP;
    public Text HP;

    // Use this for initialization
    void Start() {
        CurHP = MaxHP = 111488;

        pcController = this.GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        BodyAnimator = BodyObj.GetComponent<Animator>();
        FaceAnimator = FaceObj.GetComponent<Animator>();
        HairAnimator = HairObj.GetComponent<Animator>();
        state = PlayerState.Wait;
        isEvade = false;
        isSkill1 = false;
        isSkill2 = false;
        isSkill3 = false;
        isSkill4 = false;
        isSkill5 = false;
        isSkill6 = false;

        Skill1_1Eff.SetActive(false);
        Skill1_2Eff.SetActive(false);
        Skill1_3Eff.SetActive(false);
        Skill1_4Eff.SetActive(false);
        WeakPointEff.SetActive(false);
        Helli01Eff.SetActive(false);
        Helli02Eff.SetActive(false);
        Helli03Eff.SetActive(false);
        GlaveShot01.SetActive(false);
        GroundEff.SetActive(false);
        Cresent01Eff.SetActive(false);
        Cresent02Eff.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        //TempS = state.ToString();

        HP.text = CurHP.ToString() + " / " + MaxHP.ToString();
        audioSource = GetComponent<AudioSource>();
        if ((state == PlayerState.Wait || state == PlayerState.Run))
            CharactorMove();
        
        SetWait();
    }

    void HPMinus(int Damage)
    {
        CurHP -= Damage;

        if(CurHP <= 0)
        {
            CurHP = 0;
        }

    }

    void SetWait()
    {
        if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo1End") ||
            BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo2End") ||
            BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo3"))
        {
            if (BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
            {
                {
                    if (state == PlayerState.Combo1Start ||
                        state == PlayerState.Combo2Start ||
                        state == PlayerState.Combo4Start) return;

                    state = PlayerState.Wait;
                    isAttack = false;
                    SetAnimatorState(0);

                    BodyAnimator.SetBool("isAttck", isAttack);
                    FaceAnimator.SetBool("isAttck", isAttack);
                    HairAnimator.SetBool("isAttck", isAttack);

                    isSkill1 = false;
                    isSkill2 = false;
                    isSkill3 = false;
                    isSkill4 = false;
                    isSkill5 = false;
                }
            }
        }
    }

    private void CharactorMove()
    {
        if (isAttack) return;

        isSkill1 = false;
        isSkill2 = false;
        isSkill3 = false;
        isSkill4 = false;
        isSkill5 = false;
        //키보드
        //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),
        //    0,
        //    Input.GetAxis("Vertical"));

        //조이스틱
        Vector3 direction = new Vector3(joystick.GetHorizentalValue(),
            0,
            joystick.GetVerticalValue());

        if (direction.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp(
                transform.forward,
                direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            transform.LookAt(transform.position + forward);
        }
        else
        {

        }
        pcController.Move(direction * runSpeed * Time.deltaTime);

        BodyAnimator.SetFloat("Speed", pcController.velocity.magnitude);
        FaceAnimator.SetFloat("Speed", pcController.velocity.magnitude);
        HairAnimator.SetFloat("Speed", pcController.velocity.magnitude);

        Speed = pcController.velocity.magnitude;
    }

    //IEnumerator 
    IEnumerator PlayCombo1()
    {
        state = PlayerState.Combo1Start;
        SetAnimatorState(1);

        isAttack = true;
        SetBoolState(isAttack);

        yield return new WaitForSeconds(0.27f);

        //if (state == PlayerState.Combo1Start && isAttack)
        //{
        //    if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo1Start"))
        //        if (BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        //        {
                    state = PlayerState.Combo1Shot;
                    SetAnimatorState(2);
        PlaySound(Random.Range(0, 3));

        yield return new WaitForSeconds(0.1f);
        Skill1_1Eff.SetActive(true);
        //        }
        //}

        yield return new WaitForSeconds(0.17f);

        state = PlayerState.Combo1End;
        SetAnimatorState(3);
        TransPos();
        Skill1_1Eff.SetActive(false);
    }

    IEnumerator PlayCombo2()
    {
        state = PlayerState.Combo2Start;
        SetAnimatorState(4);

        isAttack = true;
        SetBoolState(isAttack);

        yield return new WaitForSeconds(0.27f);

        state = PlayerState.Combo2Shot;
        PlaySound(Random.Range(0, 3));
        SetAnimatorState(5);

        yield return new WaitForSeconds(0.1f);

        Skill1_2Eff.SetActive(true);

        yield return new WaitForSeconds(0.17f);

        state = PlayerState.Combo2End;
        SetAnimatorState(6);
        TransPos();
        Skill1_2Eff.SetActive(false);
    }

    IEnumerator PlayCombo3()
    {
        state = PlayerState.Combo3;
        SetAnimatorState(7);
        isAttack = true;
        SetBoolState(isAttack);

        yield return new WaitForSeconds(0.25f);
        PlaySound(Random.Range(3, 6));
        yield return new WaitForSeconds(0.25f);
        Skill1_3Eff.SetActive(true);

        yield return new WaitForSeconds(0.6f);

        Skill1_3Eff.SetActive(false);
        TransPos();
    }

    IEnumerator PlayCombo4()
    {
        state = PlayerState.Combo4Start;
        SetAnimatorState(8);

        isAttack = true;
        SetBoolState(isAttack);

        TransPos();

        yield return new WaitForSeconds(0.1f);
        PlaySound(6);
        yield return new WaitForSeconds(0.4f);

        if (state == PlayerState.Combo4Start && isAttack)
        {
            if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo4Start"))
                if (BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    state = PlayerState.Combo4Shot;
                    SetAnimatorState(9);
                    TransPos();
                }
        }

        yield return new WaitForSeconds(0.25f);
        Skill1_4Eff.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        
        if (state == PlayerState.Combo4Shot && isAttack)
        {
            if (BodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyCombo4Shot"))
                if (BodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    state = PlayerState.Combo4End;
                    SetAnimatorState(10);
                    TransPos();
                }
        }

        yield return new WaitForSeconds(1f);
        Skill1_4Eff.SetActive(false);
        yield return new WaitForSeconds(1f);

        TransPos();
        state = PlayerState.Wait;
        SetAnimatorState(0);

        isAttack = false;
        SetBoolState(isAttack);
        
    }

    public void SetAnimatorState(int StateNum)
    {
        BodyAnimator.SetInteger("State", StateNum);
        FaceAnimator.SetInteger("State", StateNum);
        HairAnimator.SetInteger("State", StateNum);
    }

    void SetBoolState(bool _isAttack)
    {
        BodyAnimator.SetBool("isAttck", _isAttack);
        FaceAnimator.SetBool("isAttck", _isAttack);
        HairAnimator.SetBool("isAttck", _isAttack);
    }

    void TransPos()
    {
        Vector3 VecRoot = RootBone.transform.position;
        Vector3 vecTemp = Vector3.zero;
        vecTemp.x = VecRoot.x;
        vecTemp.z = VecRoot.z;
        this.transform.position = vecTemp;
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
        SetBoolState(isAttack);
        PlaySound(Random.Range(7, 10));

        state = PlayerState.EvadeStart;
        SetAnimatorState(11);

        yield return new WaitForSeconds(0.25f);

        TransPos();
        state = PlayerState.EvadeShot;
        SetAnimatorState(12);

        yield return new WaitForSeconds(0.5f);

        TransPos();
        state = PlayerState.EvadeEnd;
        SetAnimatorState(13);

        yield return new WaitForSeconds(0.4f);

        isEvade = false;
        isSkill2 = false;
        TransPos();
        state = PlayerState.Wait;
        SetAnimatorState(0);
    }

    public void WeakPoint()
    {
        isSkill3 = true;
        StartCoroutine(WeakPointPlay());
    }

    IEnumerator WeakPointPlay()
    {
        isAttack = true;
        SetBoolState(isAttack);

        state = PlayerState.WeakPointStart;
        SetAnimatorState(14);
        PlaySound(10);

        yield return new WaitForSeconds(1f);

        //TransPos();
        state = PlayerState.WeakPointShot;
        SetAnimatorState(15);

        yield return new WaitForSeconds(0.2f);
        WeakPointEff.SetActive(true);
        yield return new WaitForSeconds(0.8f);

        TransPos();
        state = PlayerState.WeakPointEnd;
        SetAnimatorState(16);
        WeakPointEff.SetActive(false);
        yield return new WaitForSeconds(1f);

        TransPos();
        state = PlayerState.Wait;
        SetAnimatorState(0);

        isAttack = false;
        SetBoolState(isAttack);
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
        SetBoolState(isAttack);

        state = PlayerState.HellicopterCutStart;
        SetAnimatorState(17);

        yield return new WaitForSeconds(0.27f);

        TransPos();
        state = PlayerState.HellicopterCutShot1;
        SetAnimatorState(18);
        PlaySound(11);

        Helli01Eff.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        TransPos();
        state = PlayerState.HellicopterCutShot2;
        SetAnimatorState(19);
        Helli01Eff.SetActive(false);
        PlaySound(12);


        yield return new WaitForSeconds(0.2f);

        TransPos();
        state = PlayerState.HellicopterCutStart;
        SetAnimatorState(17);

        yield return new WaitForSeconds(0.25f);

        Helli02Eff.SetActive(true);

        TransPos();
        state = PlayerState.HellicopterCutShot1;
        SetAnimatorState(18);
        PlaySound(11);

        yield return new WaitForSeconds(0.25f);

        TransPos();
        state = PlayerState.HellicopterCutShot2;
        SetAnimatorState(19);

        Helli02Eff.SetActive(false);
        PlaySound(12);

        yield return new WaitForSeconds(0.2f);

        TransPos();
        state = PlayerState.HellicopterCutShot3;
        SetAnimatorState(20);
        PlaySound(13);

        yield return new WaitForSeconds(0.85f);
        Helli03Eff.SetActive(true);
        yield return new WaitForSeconds(0.15f);

        TransPos();
        state = PlayerState.HellicopterCutShot3End;
        SetAnimatorState(21);


        yield return new WaitForSeconds(0.5f);

        Helli03Eff.SetActive(false);

        TransPos();
        state = PlayerState.Wait;
        SetAnimatorState(0);

        isAttack = false;
        SetBoolState(isAttack);
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
        SetBoolState(isAttack);

        state = PlayerState.ThrowGlaveStart;
        SetAnimatorState(22);
        PlaySound(14);

        yield return new WaitForSeconds(0.5f);
        GlaveShot01.SetActive(true);
        TransPos();
        state = PlayerState.ThrowGlaveJump;
        SetAnimatorState(23);
        PlaySound(15);
        yield return new WaitForSeconds(0.5f);

        TransPos();
        state = PlayerState.ThrowGlaveShotStart;
        SetAnimatorState(24);
        GlaveShot01.SetActive(false);
        
        yield return new WaitForSeconds(0.25f);

        GroundEff.SetActive(true);

        yield return new WaitForSeconds(0.25f);
        TransPos();
        state = PlayerState.ThrowGlaveShot;
        SetAnimatorState(25);
       

        yield return new WaitForSeconds(0.5f);
        TransPos();
        state = PlayerState.ThrowGlaveShotEnd;
        SetAnimatorState(26);

        GroundEff.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        TransPos();
        state = PlayerState.Wait;
        SetAnimatorState(0);

        isAttack = false;
        SetBoolState(isAttack);
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
        SetBoolState(isAttack);

        state = PlayerState.BodyCrescentCut1Start;
        SetAnimatorState(27);
        
        yield return new WaitForSeconds(0.5f);

        TransPos();
        state = PlayerState.BodyCrescentCut1Shot;
        SetAnimatorState(28);
        PlaySound(16);

        yield return new WaitForSeconds(0.1f);
        Cresent01Eff.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        Cresent01Eff.SetActive(false);
        state = PlayerState.BodyCrescentCut1End;
        SetAnimatorState(29);

        yield return new WaitForSeconds(0);

        TransPos();
        state = PlayerState.BodyCrescentCut1R;
        SetAnimatorState(30);

        yield return new WaitForSeconds(0.5f);

        state = PlayerState.BodyCrescentCut2Start;
        SetAnimatorState(31);

        yield return new WaitForSeconds(0.7f);
        PlaySound(17);

        yield return new WaitForSeconds(0.6f);

        TransPos();
        state = PlayerState.BodyCrescentCut2Shot;
        SetAnimatorState(32);
        Cresent02Eff.SetActive(true);
       

        yield return new WaitForSeconds(0.5f);

        Cresent02Eff.SetActive(false);
        TransPos();
        state = PlayerState.BodyCrescentCut2End;
        SetAnimatorState(33);

        yield return new WaitForSeconds(0.5f);

        TransPos();
        state = PlayerState.BodyCrescentCut2R;
        SetAnimatorState(34);

        yield return new WaitForSeconds(0.5f);

        TransPos();
        state = PlayerState.Wait;
        SetAnimatorState(0);

        isAttack = false;
        SetBoolState(isAttack);
        isSkill6 = false;
    }

    void PlaySound(int Num)
    {
        audioSource.clip = AudioList[Num];
        audioSource.Play();
    }
}
