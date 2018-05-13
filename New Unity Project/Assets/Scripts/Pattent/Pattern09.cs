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
    private List<Tweener> _tweener = new List<Tweener>();

    private List<PATTERN09INFO> infolist = new List<PATTERN09INFO>();

    private int _waveCount;
    private float _animationTime;
    private float _bulletAnimationTime;
    private float _animationAngle;

    private AnimationCurve _animationCurve = null;


    public Pattern09(int _waveCount, float _animationTime, float _bulletAnimationTime, float _animationAngle, AnimationCurve _animationCurve)
    {
        this._waveCount = _waveCount;
        this._animationTime = _animationTime;
        this._bulletAnimationTime = _bulletAnimationTime;
        this._animationAngle = _animationAngle;
        this._animationCurve = _animationCurve;
    }

    public void OnStart()
    {
        setPatten1Info(_waveCount);

        //for (int i = 0; i < infolist.Count; ++i)
        //{
        //    setTweenAngle(i);
        //}
    }

    int _direction = 1;
    public void OnUpdate(float deletaTime)
    {
        bool bulletActivat = false;

        for (int i = 0; i < infolist.Count; ++i)
        {
            infolist[i].CreateTimeCount += deletaTime;
            if (infolist[i].CreateTimeCount >= infolist[i].CreateTime)
            {
                setTweenAngle(i);

                BasicBullet temp = BulletManager.GetInstance().GetBullet();

                temp.OnActive(infolist[i].TweenAngle, 730f, _bulletAnimationTime);

                infolist[i].listMyBullet.Add(temp);
                infolist[i].CreateTimeCount = 0f;

                bulletActivat = true;
            }
            
            for (int j = 0; j < infolist[i].listMyBullet.Count; ++j)
            {
                infolist[i].listMyBullet[j].SetBulletAngleInfo(infolist[i].TweenAngle);
            }
        }

        if (bulletActivat)
            _direction *= -1;
    }

    public void OnEnd()
    {

    }

    public bool IsTweening()
    {
        if (_tweener.Count <= 0)
            return true;

        for (int i = 0; i < _tweener.Count; ++i)
        {
            if (_tweener[i].IsPlaying())
                return true;
        }
        return false;
    }

    void setTweenAngle(int index)
    {
        int direction = _direction;

        _tweener.Add(DOTween.To(() => infolist[index].TweenAngle, x => infolist[index].TweenAngle = x, infolist[index].TweenAngle + (_animationAngle * _direction), _animationTime).SetEase(_animationCurve).OnComplete(() =>
              {
                  infolist.Clear();
              }));
    }

    void setPatten1Info(int BulletWaveCount)
    {
        float WaveStartAngle = 360f / BulletWaveCount;
        for (int i = 0; i < BulletWaveCount; ++i)
        {
            PATTERN09INFO info = new PATTERN09INFO();

            info.TweenAngle = WaveStartAngle * (i + 1);
            info.CreateTime = 0.5f;
            infolist.Add(info);
        }
    }
}
