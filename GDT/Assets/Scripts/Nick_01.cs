using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nick_01 : MonoBehaviour {

    public UIInput input;
    _e Nick = new _e();

    public UILabel FadeIn;
    public UILabel FadeOut;

	// Use this for initialization
	void Start () {
        StartCoroutine(FadeInText());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveNickName()
    {
        if (input.text == null) return;

        StartCoroutine(FadeOutText());
    }

    IEnumerator FadeInText()
    {
        FadeIn.color = new Color(0, 0, 0, 1);
        FadeOut.color = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0.9f);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0.8f);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0.7f);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0.6f);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0.5f);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0.4f);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0.3f);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0.2f);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0.1f);

        yield return new WaitForSeconds(0.05f);
        FadeIn.color = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(0.05f);
        FadeIn.gameObject.SetActive(false);
    }

    IEnumerator FadeOutText()
    {
        FadeOut.color = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 0.1f);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 0.2f);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 0.3f);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 0.4f);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 0.5f);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 0.6f);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 0.7f);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 0.8f);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 0.9f);

        yield return new WaitForSeconds(0.05f);
        FadeOut.color = new Color(0, 0, 0, 1);

        //yield return new WaitForSeconds(0.05f);
        Nick._s = input.text;
        NickSaveScript.e.Add(Nick);
        GameManager.Instance.ChangeScene("GDT");
    }
}
