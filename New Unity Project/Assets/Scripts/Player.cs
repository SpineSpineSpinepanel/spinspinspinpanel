using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float fSpeed = 0f;
    public float fRadius;

    private float _fAngle = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow))
        {
            _fAngle += fSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            _fAngle -= fSpeed * Time.deltaTime;
        }

        Vector3 vPos = Vector3.zero;

        vPos.x = 0f + (fRadius * (float)Mathf.Cos(_fAngle * Mathf.PI));
        vPos.y = 0f + (fRadius * (float)Mathf.Sin(_fAngle * Mathf.PI));
        vPos.z = -1f;

        transform.localPosition = vPos;
        transform.localEulerAngles = new Vector3(0f, 0f, (_fAngle * Mathf.PI) * (180 / Mathf.PI));
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag.Equals("Ball"))
        {
            Debug.Log("Die");
        }
    }
}
