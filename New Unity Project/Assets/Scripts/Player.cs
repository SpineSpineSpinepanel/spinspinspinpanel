using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public float Speed = 0f;
    public float Radius;
    public float SpinSpeed;
    public ParticleSystem particle_Die;

    private float _angle = 0f;

    public bool Isimmortal = false;

    public float Angle
    {
        get
        {
            return _angle;
        }

        set
        {
            _angle = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        transform.DORotate(new Vector3(0f, 0f, 360f), SpinSpeed).SetLoops(-1).SetRelative().SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetInstance().IsDie)
            return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Angle += Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Angle -= Speed * Time.deltaTime;
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 mousePoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (mousePoint.x < 0)
                Angle += Speed * Time.deltaTime;

            else if(mousePoint.x > 0)
                Angle -= Speed * Time.deltaTime;
        }

        Vector3 vPos = Vector3.zero;

        vPos.x = 0f + (Radius * (float)Mathf.Cos(Angle * Mathf.PI));
        vPos.y = 0f + (Radius * (float)Mathf.Sin(Angle * Mathf.PI));
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

    public float GetAngle()
    {
        return (_angle * Mathf.PI) * (180 / Mathf.PI);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Isimmortal && collision.tag.Equals("Ball"))
        {
            gameObject.SetActive(false);
            particle_Die.gameObject.transform.localPosition = transform.localPosition;
            particle_Die.Play();
            GameManager.GetInstance().IsDie = true;
            UIManager.GetInstance().OnDie();
        }
    }
}
