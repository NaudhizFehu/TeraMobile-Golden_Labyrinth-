using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour {

    public Image HPBar;
    public PlayerControl Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        HPBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            (Player.CurHP / Player.MaxHP) * 430);
	}
}
