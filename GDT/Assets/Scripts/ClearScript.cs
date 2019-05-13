using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearScript : MonoBehaviour {

    public Image ClearUI;

	// Use this for initialization
	void Start () {
        ClearUI.gameObject.SetActive(false);
        ClearUI.color = new Color(0, 0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        ClearUI.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
	}

    void Faderevers()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        ClearUI.gameObject.SetActive(true);
        ClearUI.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 0.1f);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 0.2f);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 0.3f);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 0.4f);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 0.6f);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 0.7f);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 0.8f);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 0.9f);

        yield return new WaitForSeconds(0.05f);
        ClearUI.color = new Color(1, 1, 1, 1.0f);
        //FadeUI.gameObject.SetActive(false);
    }
}
