using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill1Touch : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public PlayerControl playerControl;
    public ForwardScript forwardScript;
    public Image SkillImg;

    public Image Skill2;
    public Image Skill3;
    public Image Skill4;
    public Image Skill5;
    public Image Skill6;

    // Use this for initialization
    void Start () {
        //SkillImg = GetComponent<Image>();
       

    }
	
	// Update is called once per frame
	void Update () {
        SkillPos(-260, 50, Skill2);

        SkillPos(-250, 160, Skill3);

        SkillPos(-175, 245, Skill4);

        SkillPos(-65, 260, Skill5);

        SkillPos(-65, 400, Skill6);
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (playerControl.isEvade == true ||
            playerControl.isSkill2 == true ||
            playerControl.isSkill3 == true ||
            playerControl.isSkill4 == true ||
            playerControl.isSkill5 == true ||
            playerControl.isSkill6 == true) return;

        playerControl.Skill1();
        forwardScript.Skill1();
        SkillImg.color = Color.gray;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        SkillImg.color = Color.white;
    }

    void SkillPos(float _x, float _y, Image Skillnum)
    {
        Vector3 Pos = this.transform.position;
        Pos.x += _x;
        Pos.y += _y;
        Skillnum.transform.position = Pos;
    }
}
