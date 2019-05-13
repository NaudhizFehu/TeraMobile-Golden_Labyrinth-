using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public Image FadeUI;


	// Use this for initialization
	void Start () {
        StartCoroutine(FadeImage());
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeImage()
    {
        FadeUI.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(0.05f);
        FadeUI.color = new Color(0, 0, 0, 0.9f);

        yield return new WaitForSeconds(0.05f);
        FadeUI.color = new Color(0, 0, 0, 0.8f);

        yield return new WaitForSeconds(0.05f);
        FadeUI.color = new Color(0, 0, 0, 0.7f);

        yield return new WaitForSeconds(0.05f);
        FadeUI.color = new Color(0, 0, 0, 0.6f);

        yield return new WaitForSeconds(0.05f);
        FadeUI.color = new Color(0, 0, 0, 0.5f);

        yield return new WaitForSeconds(0.05f);
        FadeUI.color = new Color(0, 0, 0, 0.4f);

        yield return new WaitForSeconds(0.05f);
        FadeUI.color = new Color(0, 0, 0, 0.3f);

        yield return new WaitForSeconds(0.05f);
        FadeUI.color = new Color(0, 0, 0, 0.2f);

        yield return new WaitForSeconds(0.05f);
        FadeUI.color = new Color(0, 0, 0, 0.1f);

        yield return new WaitForSeconds(0.05f);
        FadeUI.gameObject.SetActive(false);
    }

   
}
