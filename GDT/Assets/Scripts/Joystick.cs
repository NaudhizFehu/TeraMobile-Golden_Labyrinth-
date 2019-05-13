using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image bgImg;
    private Image joystickImg;
    private Vector3 inputVector;

	// Use this for initialization
	void Start () {

        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void OnDrag(PointerEventData ped)
    {
        //Debug.Log("JoyStick >>> OnDrag()");

        Vector2 Pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out Pos))
        {
            Pos.x = (Pos.x / bgImg.rectTransform.sizeDelta.x);
            Pos.y = (Pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(Pos.x * 2 - 1, 0, Pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //Move Joystick IMG
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
                                                                     inputVector.z * (bgImg.rectTransform.sizeDelta.y / 3));

            Debug.Log(inputVector.ToString())
;        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        Vector3 VecTemp = Vector3.zero;
        VecTemp.x = 50;
        VecTemp.y = 50;

        inputVector = Vector3.zero;
        //inputVector = VecTemp;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float GetHorizentalValue()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }
    
    public float GetVerticalValue()
    {
        if (inputVector.x != 0)
            return inputVector.z;
        else return Input.GetAxis("Vertical");
    }

    
}
