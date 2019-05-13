using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JomporiControl : MonoBehaviour {

    public AudioClip[] AudioList;
    private AudioSource audioSource;

    public GameObject RootBone;
    public GameObject Target;
    public GameObject Player;
    NavMeshAgent agent;
    public Animator animator;

    public bool isFindPlayer = false;
    public Quaternion SaveRot;

    public float TargetLength = 0.0f;

    private bool isAttack = false;

    private float CurHP = 10442;
    bool Death = false;

    // Use this for initialization
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("HP", CurHP);

        Vector3 RotVec = Vector3.zero;
        RotVec.y = Random.Range(0, 360);

        this.gameObject.transform.Rotate(RotVec);
        SaveRot = this.gameObject.transform.rotation;

        agent.speed = 10f;

       // Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);
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

        if (isFindPlayer)
        {
            if (!isAttack)
            {
                if (TargetLength >= 5.5f && Target != null)
                {
                    agent.destination = Target.transform.position;
                    animator.SetFloat("Speed", agent.speed);
                }
                else
                {
                    StartCoroutine(Attack_01());
                }
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
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
        PlayAudioOneShot(3);

        yield return new WaitForSeconds(8f);

        this.gameObject.SetActive(false);
    }

    IEnumerator Attack_01()
    {
        isAttack = true;
        animator.SetBool("isAttack", true);
        animator.SetInteger("State", 1);
        PlayAudio(2);

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

    void TransPos()
    {
        Vector3 VecRoot = RootBone.transform.position;
        Vector3 vecTemp = Vector3.zero;
        vecTemp.x = VecRoot.x;
        vecTemp.z = VecRoot.z;
        this.transform.position = vecTemp;
    }
}
