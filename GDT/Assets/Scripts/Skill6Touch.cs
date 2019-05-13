using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill6Touch : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public PlayerControl playerControl;
    public ForwardScript forwardScript;
    public Image SkillImg;
    public Image TimeImg;

    float SkillTime = 0.0f;

    public bool isTouch = false;

    // Use this for initialization
    void Start()
    {
        TimeImg.type = Image.Type.Filled;
        TimeImg.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //if (isTouch)
        //{
        //    SkillTime += Time.deltaTime;
        //
        //    TimeImg.fillAmount -= Time.deltaTime / 3;
        //
        //    if (SkillTime >= 3)
        //    {
        //        isTouch = false;
        //        SkillTime = 0.0f;
        //        TimeImg.fillAmount = 1;
        //        TimeImg.gameObject.SetActive(false);
        //    }
        //}
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (playerControl.isEvade == true ||
            playerControl.isSkill1 == true ||
            playerControl.isSkill2 == true ||
            playerControl.isSkill3 == true ||
            playerControl.isSkill4 == true ||
            playerControl.isSkill5 == true) return;
        //playerControl.Skill1();
        if (!isTouch)
        {
            SkillImg.color = Color.gray;
            isTouch = true;
            TimeImg.gameObject.SetActive(true);
            TimeImg.fillAmount = 1;
            playerControl.CrescentCut();
            forwardScript.CrescentCut();
        }

    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        SkillImg.color = Color.white;
    }
}
