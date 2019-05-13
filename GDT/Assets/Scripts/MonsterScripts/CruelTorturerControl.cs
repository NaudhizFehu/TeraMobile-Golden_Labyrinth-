using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CruelTorturerControl : MonoBehaviour {

    public GameObject RootBone;
    public GameObject Target;
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

    public AudioClip[] AudioList;
    private AudioSource audioSource;

    public void HPM(int Damage)
    {
       CurHP -= Damage;

        Debug.Log("HP감소");

        animator.SetFloat("HP", CurHP);
        //animator.SetBool("isSearchPlayer", false);
        //Target = null;

    }

	// Use this for initialization
	void Start () {

        CurHP = MaxHP = 153508;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        SaveRot = this.gameObject.transform.rotation;
        animator.SetFloat("HP", CurHP);

        agent.speed = 10f;
	}
	
	// Update is called once per frame
	void Update () {

        if (CurHP <= 0)
        {
            Target = null;
            animator.SetBool("isSearchPlayer", false);
            if(!Death)
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
            if(!isAttack)
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
        if (audioSource == null) return;

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
        animator.SetInteger("AttackNum", 1);
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
        animator.SetInteger("AttackNum", 0);
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
        animator.SetInteger("AttackNum", 2);
        PlayAudio(3);

        this.transform.LookAt(Target.transform);

        yield return new WaitForSeconds(2.5f);

        animator.SetInteger("AttackNum", 3);
        PlayAudio(4);

        yield return new WaitForSeconds(2f);

        animator.SetFloat("Speed", 0);
        animator.SetBool("isAttack", false);
        animator.SetInteger("AttackNum", 0);
        TransPos();
        agent.Stop();

        yield return new WaitForSeconds(0.1f);

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
