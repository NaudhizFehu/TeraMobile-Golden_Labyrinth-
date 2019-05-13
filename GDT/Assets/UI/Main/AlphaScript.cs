using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaScript : MonoBehaviour {

    float alphaValue = 0.0f;
    bool isValueUP = true;

    public UILabel TestLabel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(isValueUP)
        {
            alphaValue += 0.01f;

            if(alphaValue >= 1)
            {
                isValueUP = false;
            }

        }
        else
        {
            alphaValue -= 0.01f;

            if (alphaValue <= 0)
            {
                isValueUP = true;
            }
        }

        TestLabel.color = new Color(0, 0, 0, alphaValue);
        //gameObject.transform.renderer.col
	}
}
