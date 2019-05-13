using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRay : MonoBehaviour {

    [Range(0, 40)]
    public float distance = 5.0f;

    private RaycastHit[] rayHits;

    public LayerMask maskValue = 8;

    public GameObject CamObj;

    public Ray CamObjRay;

    public GameObject[] ColObjList;
    private float reObjTime = 0.0f;

   // Material mtl;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CamObjRay = new Ray();
        CamObjRay.origin = CamObj.transform.position;
        CamObjRay.direction = CamObj.transform.forward;

        rayHits = Physics.RaycastAll(CamObjRay, distance, maskValue);

        reObjTime += Time.deltaTime;
        if(reObjTime >= 1.0f)
        {
            for (int j = 0; j < ColObjList.Length; j++)
            {
                //ColObjList[i].SetActive(true);
                MeshRenderer render1 = ColObjList[j].gameObject.GetComponent<MeshRenderer>();
                if(!render1)
                    continue;
                //Material mtl = render1.material.GetTexture(;
                //Texture tex = render1.material.GetTexture(0);
                //tex.
                render1.enabled = true;
                //mtl.color = new Color(mtl.color.r, mtl.color.g, mtl.color.b, 1f);
            }
        }
        //if (rayHits[0].collider.gameObject != null) return;



        for (int i = 0; i < this.rayHits.Length; i++)
        {
            //rayHits[i].collider.gameObject.SetActive(false);
            MeshRenderer render = rayHits[i].collider.gameObject.GetComponent<MeshRenderer>();
            //Material mtl = render.material;

            //mtl.color = new Color(mtl.color.r, mtl.color.g, mtl.color.b, 0.3f);
            render.enabled = false;

            //if (rayHits[i].collider == null)
            //{
            //   
            //}
        }


        
    }

    void OnDrawGizmos()
    {
        ComGizmo();
    }

    void ComGizmo()
    {
        if (this.rayHits != null && this.rayHits.Length > 0)    /// : null 비교 안해서 추가
        {
            for (int i = 0; i < this.rayHits.Length; i++)
            {
                if (this.rayHits[i].collider != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(
                        this.rayHits[i].point, 1.0f
                        );

                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(
                        CamObj.transform.position,
                        CamObj.transform.position +
                        CamObj.transform.forward * rayHits[i].distance
                        );

                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(this.rayHits[i].point,
                        this.rayHits[i].point + this.rayHits[i].normal          /// : 노말 벡터 
                        );

                    Gizmos.color = new Color(1.0f, 0.0f, 1.0f);
                    Vector3 reflect =                                       /// : 반사 벡터 구하기
                        Vector3.Reflect(CamObj.transform.forward,
                        this.rayHits[i].normal);

                    Gizmos.DrawLine(this.rayHits[i].point,
                        this.rayHits[i].point + reflect);               /// : 반사됬을때의 벡터
                }
                else
                {
                    Gizmos.DrawLine(this.transform.position,
                        CamObj.transform.position +
                        CamObj.transform.forward * this.distance
                        );
                
                }
            }

        }
    }
}
