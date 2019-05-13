using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenScript : MonoBehaviour {

    public Camera m_pCam;        // 카메라
    void Start()
    {
        UIRoot root = gameObject.GetComponent<UIRoot>();
        root.automatic = true;        // 자동 조절되게 하고.

        root.manualHeight = 480;
        root.minimumHeight = 480;
        root.maximumHeight = 1280;
    }

    // Update is called once per frame
    void Update()
    {
        float perx = 2048.0f / Screen.width;        // 이미지 사이즈 넣는다.
        float pery = 1024.0f / Screen.height;        // 요것도 이미지 사이즈
        float v = (perx > pery) ? perx : pery;

        m_pCam.GetComponent<Camera>().orthographicSize = v;
    }
}
