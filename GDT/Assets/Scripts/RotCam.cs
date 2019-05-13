using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotCam : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    Vector2 TouchPos;
    public Image RotCamPanel;
    Vector3 inputVector;

    public GameObject CamObj;

	// Use this for initialization
	void Start () {
        RotCamPanel = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 Pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(RotCamPanel.rectTransform, ped.position, ped.pressEventCamera, out Pos))
        {
            //Debug.Log(Pos.ToString());

            Pos.x = (Pos.x / RotCamPanel.rectTransform.sizeDelta.x);
            Pos.y = (Pos.y / RotCamPanel.rectTransform.sizeDelta.y);
            
            inputVector = new Vector3(Pos.x * 2 - 1, 0, Pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            ////Move Joystick IMG
            RotCamPanel.rectTransform.anchoredPosition = new Vector3(inputVector.x * (RotCamPanel.rectTransform.sizeDelta.x / 5),0,0);
            

            Vector3 RotVec = Vector3.zero;
            //RotVec.y = TouchPos.x - inputVector.x;
            //RotVec.y = RotCamPanel.rectTransform.anchoredPosition.x;
            //RotVec.Normalize();
            //RotVec *= 10.0f;
            if(TouchPos.x < inputVector.x)
            {
                RotVec.y += 5;
            }
            else if(TouchPos.x > inputVector.x)
            {
                RotVec.y -= 5;
            }

            CamObj.transform.Rotate(RotVec);

            TouchPos = inputVector;

            Debug.Log("inputVector : " + RotCamPanel.rectTransform.anchoredPosition.ToString());

            //Debug.Log("Pos : " + Pos.ToString());
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        //OnDrag(ped);
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(RotCamPanel.rectTransform, ped.position, ped.pressEventCamera, out TouchPos))
        {
            //RotCamPanel.rectTransform.anchoredPosition = TouchPos;
        }
        

          
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        //inputVector = VecTemp;
        RotCamPanel.rectTransform.anchoredPosition = Vector3.zero;
    }
}
