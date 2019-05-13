using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FuryMaligothControl : MonoBehaviour {

    enum FuryMaligothState
    {
        UnarmedWait = 0,
        Walk,
        Atk01,
        Atk03,
    }

    public AudioClip[] AudioList;
    private AudioSource audioSource;

    public GameObject RotWall_01;
    public GameObject RotWall_02;

    public bool bGo;
    public bool bBack;

    bool bTransPos = false;

    public GameObject RootBone;
    public GameObject Target;
    public GameObject Player;
    NavMeshAgent agent;
    public Animator animator;

    public bool isFindPlayer = false;
    public Quaternion SaveRot;

    public float TargetLength = 0.0f;

    private bool isAttack = false;

    private float CurHP = 188841;
    private bool Death = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("HP", CurHP);

        bGo = true;
        bBack = false;
        agent.speed = 10;

        this.transform.LookAt(RotWall_01.transform);


    }

    // Update is called once per frame
    void Update()
    {
        if (CurHP <= 0)
        {
            Target = null;
            animator.SetBool("isSearchPlayer", false);
            if (!Death)
            {
                Death = true;
                StopAllCoroutines();
                StartCoroutine(ActiveFalse());
            }
        }

        if (Target != null)
        {
            TargetLength = (this.transform.position - Target.transform.position).magnitude;
            animator.SetFloat("Length", TargetLength);
        }

        if (isFindPlayer == false)
        {
            if (bGo && !bBack)
            {
                StartCoroutine(GoWall01());

            }
            else if (!bGo && bBack)
            {
                StartCoroutine(GoWall02());
            }

            if (bTransPos)
                this.transform.position += this.transform.forward * 5 * Time.deltaTime;
        }
        else if (isFindPlayer == true)
        {
            StopCoroutine(GoWall01());
            StopCoroutine(GoWall02());

            if (!isAttack)
            {
                if (TargetLength >= 5.5f && Target != null)
                {
                    agent.destination = Target.transform.position;
                    animator.SetFloat("Speed", agent.speed);
                }
                else
                {
                    float RandAttack = Random.Range(0, 2);
                    if (RandAttack == 0)
                    {
                        StartCoroutine(Attack_01());
                    }
                    else if (RandAttack == 1)
                    {
                        StartCoroutine(Attack_02());
                    }
                }
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            RunPlayAudio();
        }
        audioSource.volume = (1.0f - ((this.transform.position - Player.transform.position).magnitude) / 100.0f);
    }

    private void RunPlayAudio()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.clip = AudioList[2];
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

    public void HPM(int Damage)
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
        PlayAudioOneShot(5);

        yield return new WaitForSeconds(8f);

        this.gameObject.SetActive(false);
    }

    IEnumerator Attack_01()
    {
        isAttack = true;
        animator.SetBool("isAttack", true);
        animator.SetInteger("State", 2);
        PlayAudio(3);

        yield return new WaitForSeconds(1f);

        this.transform.position = RootBone.transform.position;
        animator.SetFloat("Speed", 0);

        yield return new WaitForSeconds(0.2f);

        animator.SetBool("isAttack", false);
        animator.SetInteger("State", 0);
        PlayAudio(0);
        agent.Stop();

        yield return new WaitForSeconds(2f);

        agent.Resume();
        isAttack = false;
    }

    IEnumerator Attack_02()
    {
        isAttack = true;
        animator.SetBool("isAttack", true);
        animator.SetInteger("State", 3);
        PlayAudio(4);

        yield return new WaitForSeconds(2f);

        this.transform.position = RootBone.transform.position;
        animator.SetFloat("Speed", 0);

        yield return new WaitForSeconds(0.2f);

        animator.SetBool("isAttack", false);
        animator.SetInteger("State", 0);
        PlayAudio(0);
        agent.Stop();

        yield return new WaitForSeconds(2f);

        agent.Resume();
        isAttack = false;
    }

    IEnumerator GoWall01()
    {
        bGo = false;
        this.transform.LookAt(RotWall_01.transform);

        yield return new WaitForSeconds(0.1f);

        animator.SetInteger("State", 1);
        PlayAudio(1);
        bTransPos = true;

        yield return new WaitForSeconds(6f);

        bTransPos = false;

        animator.SetInteger("State", 0);
        PlayAudio(0);

        yield return new WaitForSeconds(2f);

        bBack = true;
    }

    IEnumerator GoWall02()
    {
        bBack = false;
        this.transform.LookAt(RotWall_02.transform);

        yield return new WaitForSeconds(0.1f);

        animator.SetInteger("State", 1);
        PlayAudio(1);
        bTransPos = true;

        yield return new WaitForSeconds(6f);

        bTransPos = false;

        animator.SetInteger("State", 0);
        PlayAudio(0);

        yield return new WaitForSeconds(2f);

        bGo = true;
    }
}
