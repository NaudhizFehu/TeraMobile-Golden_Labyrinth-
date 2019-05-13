using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElementalKumasControl : MonoBehaviour {

    public AudioClip[] AudioList;
    private AudioSource audioSource;

    public GameObject RootBone;
    public GameObject Target;
    public GameObject ResetPoint;
    public GameObject Player;
    NavMeshAgent agent;
    public Animator animator;

    public bool isFindPlayer = false;
    public Quaternion SaveRot;

    public float TargetLength = 0.0f;

    private bool isAttack = false;

    public float CurHP;
    public float MaxHP;
    private bool Death = false;

    private TrailRenderer LHand;
    private TrailRenderer RHand;

    public GameObject LeftHand;
    public GameObject RightHand;

    public GameObject fade;
    public GameObject EndingObj;
    // Use this for initialization
    void Start () {
        CurHP = MaxHP = 780000;
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        SaveRot = this.gameObject.transform.rotation;
        animator.SetFloat("HP", CurHP);

        LHand = LeftHand.GetComponent<TrailRenderer>();
        RHand = RightHand.GetComponent<TrailRenderer>();

        agent.speed = 10f;
    }
	
	// Update is called once per frame
	void Update () {

        if (CurHP <= 0)
        {
            Target = null;
            animator.SetBool("isSearchPlayer", false);
            if (!Death)
            {
                Death = true;
                StartCoroutine(ActiveFalse());
            }
        }

        if((this.transform.position - ResetPoint.transform.position).magnitude >= 30)
        {
            Target = null;
            isFindPlayer = false;
            animator.SetBool("isSearchPlayer", false);
            this.transform.position = ResetPoint.transform.position;
            agent.destination = ResetPoint.transform.position;
        }

        if (Target != null)
        {
            TargetLength = (this.transform.position - Target.transform.position).magnitude;
            animator.SetFloat("Length", TargetLength);

        }

        if (isFindPlayer)
        {
            if (!isAttack)
            {
                if (TargetLength >= 10 && Target != null)
                {
                    agent.destination = Target.transform.position;
                    animator.SetFloat("Speed", agent.speed);
                }
                else
                {
                    float RandAttack = Random.Range(0, 3);
                    if (RandAttack == 0)
                    {
                        StartCoroutine(Attack_01());
                    }
                    else if (RandAttack == 1)
                    {
                        StartCoroutine(Attack_02());
                    }
                    else if (RandAttack == 2)
                    {
                        StartCoroutine(Attack_03());
                    }
                    else if (RandAttack == 3)
                    {
                        StartCoroutine(Attack_04());
                    }
                }
            }
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            RunPlayAudio();
        }
        audioSource.volume = (1.0f - ((this.transform.position - Player.transform.position).magnitude) / 100.0f) / 2;
    }

    private void RunPlayAudio()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.clip = AudioList[1];
            audioSource.Play();
        }
    }

    private void PlayAudio(int index)
    {
        //if (audioSource.isPlaying == false)
        {
            audioSource.clip = AudioList[index];
            audioSource.Play();
        }
    }
    private void PlayAudioOneShot(int index)
    {
        //if (audioSource.isPlaying == false)
        {
            //audioSource.clip = AudioList[index];
            audioSource.PlayOneShot(AudioList[index]);
        }
    }

    public void HPMinus(int Damage)
    {
        CurHP -= Damage;

        Debug.Log("HP감소");

        animator.SetFloat("HP", CurHP);
        //animator.SetBool("isSearchPlayer", false);
        //Target = null;

    }

    IEnumerator ActiveFalse()
    {
        audioSource.Stop();
        PlayAudioOneShot(6);
        LHand.gameObject.SetActive(false);
        RHand.gameObject.SetActive(false);
        EndingObj.SendMessage("EndingActive");

        yield return new WaitForSeconds(15f);

        this.gameObject.SetActive(false);
        fade.gameObject.SetActive(true);
        fade.SendMessage("Faderevers");
    }

    IEnumerator Attack_01()
    {
        isAttack = true;
        animator.SetBool("isAttack", true);
        animator.SetInteger("State", 1);
        PlayAudio(2);

        if (animator.GetAnimatorTransitionInfo(0).IsName("Atk01"))
        {
            if (animator.GetAnimatorTransitionInfo(0).normalizedTime <= 0.1)
            {
                this.transform.LookAt(Target.transform);
            }
        }

        yield return new WaitForSeconds(2f);

        animator.SetFloat("Speed", 0);
        animator.SetBool("isAttack", false);
        animator.SetInteger("State", 0);
        PlayAudio(0);
        TransPos();
        agent.Stop();


        yield return new WaitForSeconds(0.1f);

        if(Target != null)
        this.transform.LookAt(Target.transform);

        yield return new WaitForSeconds(0.5f);

        isAttack = false;
        animator.SetBool("isAttack", false);

        agent.Resume();
    }

    IEnumerator Attack_02()
    {
        isAttack = true;
        animator.SetBool("isAttack", true);
        animator.SetInteger("State", 2);
        PlayAudio(3);

        if (animator.GetAnimatorTransitionInfo(0).IsName("Atk02"))
        {
            if (animator.GetAnimatorTransitionInfo(0).normalizedTime <= 0.1)
            {
                this.transform.LookAt(Target.transform);
            }
        }

        yield return new WaitForSeconds(2.25f);

        animator.SetFloat("Speed", 0);
        animator.SetBool("isAttack", false);
        animator.SetInteger("State", 0);
        PlayAudio(0);
        TransPos();
        agent.Stop();

        yield return new WaitForSeconds(0.1f);
        
        if(Target != null)
        this.transform.LookAt(Target.transform);
        //animator.SetBool("isAttack", false);
        
        yield return new WaitForSeconds(1.0f);
        //
        isAttack = false;
        agent.Resume();
    }

    IEnumerator Attack_03()
    {
        isAttack = true;
        animator.SetBool("isAttack", true);
        animator.SetInteger("State", 3);
        PlayAudio(4);

        if (animator.GetAnimatorTransitionInfo(0).IsName("HeavyAtk"))
        {
            if (animator.GetAnimatorTransitionInfo(0).normalizedTime <= 0.1)
            {
                this.transform.LookAt(Target.transform);
            }
        }

        yield return new WaitForSeconds(3f);

        animator.SetFloat("Speed", 0);
        animator.SetBool("isAttack", false);
        animator.SetInteger("State", 0);
        PlayAudio(0);
        TransPos();
        agent.Stop();


        yield return new WaitForSeconds(0.1f);

        if(Target != null)
        this.transform.LookAt(Target.transform);

        yield return new WaitForSeconds(0.5f);

        isAttack = false;
        animator.SetBool("isAttack", false);

        agent.Resume();
    }

    IEnumerator Attack_04()
    {
        isAttack = true;
        animator.SetBool("isAttack", true);
        animator.SetInteger("State", 4);
        PlayAudio(5);

        if (animator.GetAnimatorTransitionInfo(0).IsName("LongAtk01_1"))
        {
            if (animator.GetAnimatorTransitionInfo(0).normalizedTime <= 0.1)
            {
                this.transform.LookAt(Target.transform);
            }
        }

        yield return new WaitForSeconds(1f);

        animator.SetFloat("Speed", 0);
        animator.SetInteger("State", 5);
        //TransPos();
        agent.Stop();


        yield return new WaitForSeconds(5.5f);

        animator.SetInteger("State", 0);
        animator.SetBool("isAttack", false);
        PlayAudio(0);

        yield return new WaitForSeconds(0.1f);

        if(Target != null)
        this.transform.LookAt(Target.transform);

        yield return new WaitForSeconds(2f);

        isAttack = false;
        //animator.SetBool("isAttack", false);

        agent.Resume();
    }

    void TransPos()
    {
        Vector3 VecRoot = RootBone.transform.position;
        Vector3 vecTemp = Vector3.zero;
        vecTemp.x = VecRoot.x;
        vecTemp.z = VecRoot.z;
        this.transform.position = vecTemp;
    }
}
