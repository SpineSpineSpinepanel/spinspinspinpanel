using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PATTERN09INFO
{
    public float TweenAngle = 0f;
    public float CreateTime = 0.1f;
    public float CreateTimeCount = 0f;

    public List<BasicBullet> listMyBullet = new List<BasicBullet>();
}

public class Pattern09 : IPattern
{
    private PATTERN09INFO info = null;

    private int _rotateCount;
    private float _animationTime;
    private float _bulletAnimationTime;
    private float _animationAngle;

    private List<Tweener> _tweener = new List<Tweener>();

    public Pattern09( float _animationTime, float _bulletAnimationTime, float _animationAngle)
    {
        //this._rotateCount = _rotateCount;
        this._animationTime = _animationTime;
        this._bulletAnimationTime = _bulletAnimationTime;
        this._animationAngle = _animationAngle;
    }

    public void OnStart()
    {
        setPatten1Info();

        //for (int i = 0; i < info.Count; ++i)
        //{
            setTweenAngle();
        //}
    }

    public void OnUpdate(float deletaTime)
    {
        info.CreateTimeCount += deletaTime;
        if (info.CreateTimeCount >= info.CreateTime)
        {
            BasicBullet temp = BulletManager.GetInstance().GetBullet();

            temp.OnActive(info.TweenAngle, 730f, _bulletAnimationTime);

            info.listMyBullet.Add(temp);
            info.CreateTimeCount = 0f;
        }
    }

    public void OnEnd()
    {
    }

    public bool IsTweening()
    {
        for (int i = 0; i < _tweener.Count; ++i)
        {
            if (_tweener[i].IsPlaying())
                return true;
        }
        return false;
    }

    void setTweenAngle()
    {
        _tweener.Add(DOTween.To(() => info.TweenAngle, x => info.TweenAngle = x, info.TweenAngle + _animationAngle, _animationTime).SetEase(Ease.Linear));
    }

    void setPatten1Info()
    {
        PATTERN09INFO info = new PATTERN09INFO();

        info.TweenAngle = 0;
        info.CreateTime = 0.1f;
    }
}
