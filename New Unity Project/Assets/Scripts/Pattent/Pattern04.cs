﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PATTERN04INFO
{
    public float TweenAngle = 0f;
    public float CreateTime = 0.1f;
    public float CreateTimeCount = 0f;

    public List<BasicBullet> listMyBullet = new List<BasicBullet>();
}

public class Pattern04 : IPattern
{
    List<PATTERN01INFO> infolist = new List<PATTERN01INFO>();

    private int _waveCount;
    private float _animationTime;
    private float _bulletAnimationTime;
    private float _animationAngle;

    private AnimationCurve _curve_Angle;

    private List<Tweener> _tweener = new List<Tweener>();

    public Pattern04(int _waveCount, float _animationTime, float _bulletAnimationTime, float _animationAngle, AnimationCurve _curve_Angle)
    {
        this._waveCount = _waveCount;
        this._animationTime = _animationTime;
        this._bulletAnimationTime = _bulletAnimationTime;
        this._animationAngle = _animationAngle;
        this._curve_Angle = _curve_Angle;
    }

    public void OnStart()
    {
        setPatten1Info(_waveCount);

        for (int i = 0; i < infolist.Count; ++i)
        {
            setTweenAngle(i, i % 2 == 0 ? 1 : -1);
        }
    }

    int nBulletCnt = 0;
    public void OnUpdate(float deletaTime)
    {
        for (int i = 0; i < infolist.Count; ++i)
        {
            infolist[i].CreateTimeCount += deletaTime;
            if (infolist[i].CreateTimeCount >= infolist[i].CreateTime)
            {
                BasicBullet temp = BulletManager.GetInstance().GetBullet();

                temp.OnActive(infolist[i].TweenAngle, 730f, _bulletAnimationTime);

                infolist[i].listMyBullet.Add(temp);
                infolist[i].CreateTimeCount = 0f;
                nBulletCnt++;
            }

            for (int j = 0; j < infolist[i].listMyBullet.Count; ++j)
            {
                infolist[i].listMyBullet[j].SetBulletAngleInfo(infolist[i].TweenAngle);
            }
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

    void setTweenAngle(int index, int direction)
    {
        if (direction == 1)
        {
            _tweener.Add(DOTween.To(() => infolist[index].TweenAngle, x => infolist[index].TweenAngle = x, infolist[index].TweenAngle + _animationAngle, _animationTime).SetEase(_curve_Angle).OnComplete(
                () =>
                {
                    infolist.Remove(infolist[index]);
                }));
        }
        else
        {
            _tweener.Add(DOTween.To(() => infolist[index].TweenAngle, x => infolist[index].TweenAngle = x, infolist[index].TweenAngle - _animationAngle, _animationTime).SetEase(_curve_Angle).OnComplete(
                () =>
                {
                    infolist.Remove(infolist[index]);
                }));
        }
    }

    void setPatten1Info(int BulletWaveCount)
    {
        float WaveStartAngle = 360f / BulletWaveCount;
        for (int i = 0; i < BulletWaveCount; ++i)
        {
            PATTERN01INFO info = new PATTERN01INFO();

            info.TweenAngle = WaveStartAngle * (i + 1);
            info.CreateTime = 0.5f;
            infolist.Add(info);
        }
    }
}
