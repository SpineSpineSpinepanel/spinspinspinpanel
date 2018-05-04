using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public float Speed = 0f;
    public float Radius;
    public float SpinSpeed;


    private float _fAngle = 0f;
    // Use this for initialization
    void Start()
    {
        transform.DORotate(new Vector3(0f, 0f, 360f), SpinSpeed).SetLoops(-1).SetRelative().SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _fAngle += Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _fAngle -= Speed * Time.deltaTime;
        }

        Vector3 vPos = Vector3.zero;

        vPos.x = 0f + (Radius * (float)Mathf.Cos(_fAngle * Mathf.PI));
        vPos.y = 0f + (Radius * (float)Mathf.Sin(_fAngle * Mathf.PI));
        vPos.z = 0f;

        transform.localPosition = vPos;
        //transform.localEulerAngles = new Vector3(0f, 0f, (_fAngle * Mathf.PI) * (180 / Mathf.PI));
    }

    //void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.tag.Equals("Ball"))
    //    {
    //        Debug.Log("Die");
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D");
        if (collision.tag.Equals("Ball"))
        {
            Debug.Log("Die");
        }
    }
}
