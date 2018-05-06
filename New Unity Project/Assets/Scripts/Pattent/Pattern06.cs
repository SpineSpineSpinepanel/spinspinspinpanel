using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PATTERN06INFO
{
    public float TweenAngle = 0f;
    public float CreateTime = 0.1f;
    public float CreateTimeCount = 0f;
    public float StartWaveTime = 0f;
    public float StartWaveTimeCount = 0f;

    public bool IsUse = false;
    public bool IsMoveUse = false;
    public List<BasicBullet> listMyBullet = new List<BasicBullet>();
}


public class Pattern06 : IPattern
{
    List<PATTERN06INFO> infolist = new List<PATTERN06INFO>();

    private int _waveCount;
    private float _animationTime;
    private float _bulletAnimationTime;
    private float _animationAngle;
    private float _startWaveTime;

    private float _startAngle = 0f;

    private AnimationCurve _curve_Angle;

    private List<Tweener> _tweener = new List<Tweener>();

    public Pattern06(int _waveCount, float _animationTime, float _bulletAnimationTime, float _animationAngle, float _startWaveTime, AnimationCurve _curve_Angle)
    {
        this._waveCount = _waveCount;
        this._animationTime = _animationTime;
        this._bulletAnimationTime = _bulletAnimationTime;
        this._animationAngle = _animationAngle;
        this._startWaveTime = _startWaveTime;
        this._curve_Angle = _curve_Angle;
    }

    public void OnStart()
    {
        setPatten1Info(_waveCount);

        //for (int i = 0; i < infolist.Count; ++i)
        //{
        //    setTweenAngle(i, i % 2 == 0 ? 1 : -1);
        //}
    }

    bool IsRight = false;
    public void OnUpdate(float deletaTime)
    {
        for (int i = 0; i < infolist.Count; ++i)
        {
            infolist[i].StartWaveTimeCount += deletaTime;
            if (infolist[i].StartWaveTimeCount >= infolist[i].StartWaveTime)
            {
                if (!infolist[i].IsUse)
                {
                    infolist[i].IsUse = true;
                    if (i == 0)
                    {
                        infolist[i].TweenAngle = GameManager.GetInstance().player.GetAngle();
                        _startAngle = GameManager.GetInstance().player.GetAngle();
                    }
                    else
                    {
                        if (i == 1)
                        {
                            if (_startAngle >= GameManager.GetInstance().player.GetAngle())
                            {
                                IsRight = true;
                            }
                            else
                            {
                                IsRight = false;
                            }
                        }
                        if (IsRight)
                            infolist[i].TweenAngle = _startAngle + (360f - (360f / infolist.Count) * i);
                        else
                            infolist[i].TweenAngle = _startAngle - (360f - (360f / infolist.Count) * i);
                    }
                }
                infolist[i].CreateTimeCount += deletaTime;
                if (infolist[i].CreateTimeCount >= infolist[i].CreateTime)
                {
                    BasicBullet temp = BulletManager.GetInstance().GetBullet();

                    temp.OnActive(infolist[i].TweenAngle, 730f, _bulletAnimationTime);

                    infolist[i].listMyBullet.Add(temp);
                    infolist[i].CreateTimeCount = 0f;
                }
            }

            if (isAllUse())
            {
                if (!infolist[i].IsMoveUse)
                {
                    infolist[i].IsMoveUse = true;
                    setTweenAngle(i);
                }
            }

            //for (int j = 0; j < infolist[i].listMyBullet.Count; ++j)
            //{
            //    infolist[i].listMyBullet[j].SetBulletAngleInfo(infolist[i].TweenAngle);
            //}
        }

    }

    bool isAllUse()
    {
        for (int i = 0; i < infolist.Count; ++i)
        {
            if (!infolist[i].IsUse)
                return false;
        }
        return true;
    }

    public void OnEnd()
    {

    }

    public bool IsTweening()
    {
        if (_tweener.Count == 0)
        {
            return true;
        }
        else
        {
            for (int i = 0; i < _tweener.Count; ++i)
            {
                if (_tweener[i].IsPlaying())
                    return true;
            }
            return false;
        }
    }

    void setTweenAngle(int index)
    {
        _tweener.Add(DOTween.To(() => infolist[index].TweenAngle, x => infolist[index].TweenAngle = x, infolist[index].TweenAngle + _animationAngle, _animationTime).SetEase(_curve_Angle).OnComplete(
        () =>
        {
            infolist.Remove(infolist[index]);
        }));

    }

    void setPatten1Info(int BulletWaveCount)
    {
        float WaveStartAngle = 360f / BulletWaveCount;
        for (int i = 0; i < BulletWaveCount; ++i)
        {
            PATTERN06INFO info = new PATTERN06INFO();

            info.TweenAngle = WaveStartAngle * (i + 1);
            info.CreateTime = 0.1f;
            info.StartWaveTime = _startWaveTime * i;
            infolist.Add(info);
        }
    }
}
