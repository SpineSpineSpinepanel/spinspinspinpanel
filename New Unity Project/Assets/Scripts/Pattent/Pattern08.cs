using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pattern08 : IPattern
{
    List<PATTERN01INFO> infolist_left = new List<PATTERN01INFO>();
    List<PATTERN01INFO> infolist_right = new List<PATTERN01INFO>();

    private int _waveCount;
    private float _animationTime;
    private float _bulletAnimationTime;
    private float _animationAngle;
    private float _leftCreateTime;
    private float _rightCreateTime;

    private AnimationCurve _curve_Angle;

    private List<Tweener> _tweener = new List<Tweener>();

    public Pattern08(int _waveCount, float _animationTime, float _bulletAnimationTime, float _animationAngle, float _leftCreateTime,float _rightCreateTime, AnimationCurve _curve_Angle)
    {
        this._waveCount = _waveCount;
        this._animationTime = _animationTime;
        this._bulletAnimationTime = _bulletAnimationTime;
        this._animationAngle = _animationAngle;
        this._leftCreateTime = _leftCreateTime;
        this._rightCreateTime = _rightCreateTime;
        this._curve_Angle = _curve_Angle;
    }

    public void OnStart()
    {
        setPatten1Info(_waveCount);

        for (int i = 0; i < infolist_left.Count; ++i)
        {
            setTweenAngle(i, 1);
        }

        for (int i = 0; i < infolist_right.Count; ++i)
        {
            setTweenAngle(i, -1);
        }


    }
    int nBulletCnt = 0;
    public void OnUpdate(float deletaTime)
    {
        for (int i = 0; i < infolist_left.Count; ++i)
        {
            infolist_left[i].CreateTimeCount += deletaTime;
            if (infolist_left[i].CreateTimeCount >= infolist_left[i].CreateTime)
            {
                BasicBullet temp = BulletManager.GetInstance().GetBullet();

                temp.OnActive(infolist_left[i].TweenAngle, 730f, _bulletAnimationTime);

                infolist_left[i].listMyBullet.Add(temp);
                infolist_left[i].CreateTimeCount = 0f;
            }

        }


        for (int i = 0; i < infolist_right.Count; ++i)
        {
            infolist_right[i].CreateTimeCount += deletaTime;
            if (infolist_right[i].CreateTimeCount >= infolist_right[i].CreateTime)
            {
                BasicBullet temp = BulletManager.GetInstance().GetBullet();

                temp.OnActive(infolist_right[i].TweenAngle, 730f, _bulletAnimationTime);

                infolist_right[i].listMyBullet.Add(temp);
                infolist_right[i].CreateTimeCount = 0f;
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
            _tweener.Add(DOTween.To(() => infolist_left[index].TweenAngle, x => infolist_left[index].TweenAngle = x, infolist_left[index].TweenAngle + _animationAngle, _animationTime).SetEase(_curve_Angle).OnComplete(
                () =>
                {
                    infolist_left.Remove(infolist_left[index]);
                }));
        }
        else
        {
            _tweener.Add(DOTween.To(() => infolist_right[index].TweenAngle, x => infolist_right[index].TweenAngle = x, infolist_right[index].TweenAngle - _animationAngle, _animationTime).SetEase(_curve_Angle).OnComplete(
                () =>
                {
                    infolist_right.Remove(infolist_right[index]);
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
            info.CreateTime = _leftCreateTime;
            infolist_left.Add(info);
        }

        for (int i = 0; i < BulletWaveCount; ++i)
        {
            PATTERN01INFO info = new PATTERN01INFO();

            info.TweenAngle = WaveStartAngle * (i + 1);
            info.CreateTime = _rightCreateTime;
            infolist_right.Add(info);
        }
    }
}
