using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicBullet : MonoBehaviour
{

    private bool _isMoveing;
    private bool _isMoveFalg = false;

    private float _radius;
    private float _radiusMoveTime;

    private float _formAgnle;
    private float _angle;
    private float _angleMoveTime;

    public bool IsMoveing
    {
        get
        {
            return _isMoveing;
        }

        set
        {
            _isMoveing = value;
        }
    }
    // startangle : 시작 각도, startradius : 시작 반지름, MoveTime : 움직이는 시간
    public void OnActive(float StartAngle, float StartRadius, float MoveTime)
    {
        gameObject.SetActive(true);
        _radius = StartRadius;
        _angle = StartAngle;

        _radiusMoveTime = MoveTime;
        _isMoveFalg = true;


        DOTween.To(() => _radius, x => _radius = x, 0f, _radiusMoveTime).SetEase(Ease.Linear).OnComplete(
             () =>
             {
                 _isMoveing = false;

                 gameObject.SetActive(false);
                 _radius = 10000f;

                 GameManager.GetInstance().SetLevelProgeress();


             }).OnUpdate(() => {
                 Vector3 vPos = Vector3.zero;


                 vPos.x = (_radius) * (float)Mathf.Cos(Mathf.Deg2Rad * _angle);
                 vPos.y = (_radius) * (float)Mathf.Sin(Mathf.Deg2Rad * _angle);
                 vPos.z = -2f;

                 transform.localPosition = vPos;
             });
    }

    public void SetBulletRadiusInfo(float radius, float radiusTime)
    {
        _radius = radius;
        _radiusMoveTime = radiusTime;
    }

    public void SetBulletAngleInfo(float Angle)
    {
        _angle = Angle;
    }

    public void OnDestory()
    {
    }

    public void OnUpdate()
    {
        if (!gameObject.activeSelf) return;

        if (_isMoveFalg)
        {
            _isMoveFalg = false;

            DOTween.To(() => _radius, x => _radius = x, 0f, _radiusMoveTime).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    _isMoveing = false;
                }).OnUpdate(()=> {
                    Vector3 vPos = Vector3.zero;

                    vPos.x = (_radius) * (float)Mathf.Cos(Mathf.Deg2Rad * _angle);
                    vPos.y = (_radius) * (float)Mathf.Sin(Mathf.Deg2Rad * _angle);
                    vPos.z = -2f;

                    transform.localPosition = vPos;
                });
        }
        else
        {
            if (!_isMoveing)
            {
                // 초기화
                gameObject.SetActive(false);
                _radius = 10000f;
            }
        }

    }
}
