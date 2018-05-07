using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Pattern07 : IPattern
{
    List<PATTERN06INFO> infolist = new List<PATTERN06INFO>();
    
    private float _animationTime;
    private float _bulletAnimationTime;
    private float _animationAngle;
    private float _startWaveTime;
    private float _startAngle;
    private int _loopCnt;

    private AnimationCurve _curve_Angle;

    private List<Tweener> _tweener = new List<Tweener>();

    public Pattern07(float _animationTime, float _bulletAnimationTime, float _animationAngle, float _startWaveTime, float _startAngle, int _loopCnt, AnimationCurve _curve_Angle)
    {
        this._animationTime = _animationTime;
        this._bulletAnimationTime = _bulletAnimationTime;
        this._animationAngle = _animationAngle;
        this._startWaveTime = _startWaveTime;
        this._startAngle = _startAngle;
        this._loopCnt = _loopCnt;
        this._curve_Angle = _curve_Angle;
    }

    public void OnStart()
    {
        setPatten1Info(2);

        //for (int i = 0; i < infolist.Count; ++i)
        //{
        //    setTweenAngle(i, i % 2 == 0 ? 1 : -1);
        //}
    }

    private float _patternTimeCheck = 0f;
    public void OnUpdate(float deletaTime)
    {
        _patternTimeCheck += deletaTime;
        for (int i = 0; i < infolist.Count; ++i)
        {
            infolist[i].StartWaveTimeCount += deletaTime;
            if (infolist[i].StartWaveTimeCount >= infolist[i].StartWaveTime)
            {
                if (!infolist[i].IsMoveUse)
                {
                    if (i == 0)
                    {
                        infolist[i].TweenAngle = GameManager.GetInstance().player.GetAngle() - _startAngle;
                    }
                    else
                    {
                        infolist[i].TweenAngle = GameManager.GetInstance().player.GetAngle() + _startAngle;
                    }
                }
                if (!infolist[i].IsUse)
                {
                    infolist[i].IsUse = true;
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

            if (_patternTimeCheck >= _startWaveTime * infolist.Count + _bulletAnimationTime)
            {
                if (isAllUse())
                {
                    if (!infolist[i].IsMoveUse)
                    {
                        infolist[i].IsMoveUse = true;
                        setTweenAngle(i);
                    }
                }
            }
            if (!infolist[i].IsMoveUse)
            {
                for (int j = 0; j < infolist[i].listMyBullet.Count; ++j)
                {
                    infolist[i].listMyBullet[j].SetBulletAngleInfo(infolist[i].TweenAngle);
                }
            }
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
        _tweener.Add(DOTween.To(() => infolist[index].TweenAngle, x => infolist[index].TweenAngle = x, infolist[index].TweenAngle + _animationAngle, _animationTime).SetEase(_curve_Angle).SetLoops(_loopCnt, LoopType.Yoyo));

    }

    void setPatten1Info(int BulletWaveCount)
    {
        float WaveStartAngle = 360f / BulletWaveCount;
        for (int i = 0; i < BulletWaveCount; ++i)
        {
            PATTERN06INFO info = new PATTERN06INFO();

            info.TweenAngle = WaveStartAngle * (i + 1);
            info.CreateTime = 0.05f;
            info.StartWaveTime = _startWaveTime * i;
            infolist.Add(info);
        }
    }
}
