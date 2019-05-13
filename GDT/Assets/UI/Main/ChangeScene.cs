using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

    public UILabel Bland;
    float AlphaValue = 0;

    // Use this for initialization
    void Start () {
        Bland.color = new Color(0, 0, 0, 0);
        //Bland.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Change()
    {

        StartCoroutine(CoChange());
        
    }

    IEnumerator CoChange()
    {
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.05f);

        AlphaValue += 0.1f;
        Bland.color = new Color(0, 0, 0, AlphaValue);
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ChangeScene("NickTest");
    }
}
