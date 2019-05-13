using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour {

    public Image HPBar;
    public ElementalKumasControl Boss;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HPBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
           (Boss.CurHP / Boss.MaxHP) * 482);
    }
}
