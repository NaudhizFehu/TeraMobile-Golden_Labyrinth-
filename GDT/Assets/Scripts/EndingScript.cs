using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour {

    public GameObject[] SoundAllList;
    public GameObject BGM;
    private AudioSource AS;

    public AudioClip AC;
    private AudioSource ThisAS;
	// Use this for initialization
	void Start () {
        AS = BGM.GetComponent<AudioSource>();
        ThisAS = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndingActive()
    {
        for (int i = 0; i < SoundAllList.Length; i++)
        {
            SoundAllList[i].gameObject.SetActive(false);
        }
        
        ThisAS.clip = AC;
        StartCoroutine(PlaySound());
        
    }
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(15f);
        AS.mute = true;
        ThisAS.Play();
    }
}
