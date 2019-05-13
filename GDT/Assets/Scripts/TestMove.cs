using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{

    float fHorizon;
    float fRotHorizon;
    float fVertical;

    float speed = 5.0f;
    float rotationspeed = 360f;
    CharacterController pcController;
    Vector3 Rot;

    Rigidbody rigidbody;

    public Camera cam;
    Vector3 CamRot;
    Vector3 CamTrans;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        pcController = GetComponent<CharacterController>();
        //CamRot.x = 30;
        // cam.transform.Rotate(CamRot);
        cam.transform.parent = pcController.transform;
    }

    // Update is called once per frame
    void Update()
    {
        fVertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Q))
        {
            fHorizon = -1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            fHorizon = 1;
        }
        
        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        {
            fHorizon = 0;
        }
        fRotHorizon = Input.GetAxis("Horizontal");
        Rot = new Vector3(0, fRotHorizon, 0);

        //fVertical = Input.GetAxis("Vertical");

        //Vector3 Pos = new Vector3(fHorizon, 0, fVertical);
        //
        ////Pos.Set(fHorizon, 0, fVertical);
        //Pos = Pos.normalized * speed * Time.deltaTime;
        //
        //rigidbody.MovePosition(transform.position + Pos);
        this.transform.Rotate(Rot);

        //CamTrans = this.transform.position;
        //CamTrans.y += 20;
        //CamTrans.z -= 30;
        //
        //cam.transform.position = CamTrans;

        CharacterControl_Slerp();
    }

    private void CharacterControl_Slerp()
    {
        Vector3 direction = new Vector3(fHorizon,
            0,
            fVertical);
        if (direction.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp(
                transform.forward,
                direction,
                rotationspeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
    
            transform.LookAt(transform.position + forward);
        }

        //this.transform.Rotate(new Vector3(0, fRotHorizon, 0));
        pcController.Move(direction * speed * Time.deltaTime);
    
        //animator.SetFloat("Speed", pcController.velocity.magnitude);
    }
}
