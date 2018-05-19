using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class PATTERN10INFO
{
    public float TweenAngle = 0f;
    public float TweenAngleReverse = 0f;
    public float CreateTime = 0.1f;
    public float CreateTimeCount = 0f;

    public List<BasicBullet> listMyBullet = new List<BasicBullet>();
}

public class Pattern10 : IPattern
{
    private int _waveCount;
    private float _animationTime;
    private float _bulletAnimationTime;
    private float _animationAngle;

    private List<Tweener> _tweener = new List<Tweener>();
    private List<PATTERN10INFO> _infoList = new List<PATTERN10INFO>();

    public Pattern10(int waveCount, float animationTime, float bulletTime, float animationAngle)
    {
        _waveCount = waveCount;
        _animationTime = animationTime;
        _bulletAnimationTime = bulletTime;
        _animationAngle = animationAngle;
    }

    public void OnStart()
    {
        SetPatternInfo();

        for (int i = 0; i < _infoList.Count; ++i)
            SetTweenAngle(i);
    }

    int _direction = 1;
    // 이제보니 매개변수 deletaTime 오타남.
    public void OnUpdate(float deletaTime)
    {
        bool bulletActivat = false;

        for (int i = 0; i < _infoList.Count; ++i)
        {
            _infoList[i].CreateTimeCount += deletaTime;
            if (_infoList[i].CreateTimeCount >= _infoList[i].CreateTime)
            {
                SetTweenAngle(i);

                BasicBullet temp = BulletManager.GetInstance().GetBullet();

                temp.OnActive(_infoList[i].TweenAngle, 730f, _bulletAnimationTime);

                _infoList[i].listMyBullet.Add(temp);
                _infoList[i].CreateTimeCount = 0f;

                bulletActivat = true;
            }

            for (int j = 0; j < _infoList[i].listMyBullet.Count; ++j)
            {
                if (j % 2 == 0)
                    _infoList[i].listMyBullet[j].SetBulletAngleInfo(_infoList[i].TweenAngle);
                else
                    _infoList[i].listMyBullet[j].SetBulletAngleInfo(_infoList[i].TweenAngleReverse);
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

    private void SetTweenAngle(int index)
    {
        _tweener.Add(DOTween.To(() => _infoList[index].TweenAngle, x => _infoList[index].TweenAngle = x, _infoList[index].TweenAngle + _animationAngle, _animationTime).OnComplete(() =>
            {
                _infoList.Clear();
            }));

        _tweener.Add(DOTween.To(() => _infoList[index].TweenAngleReverse, x => _infoList[index].TweenAngleReverse = x, _infoList[index].TweenAngleReverse - _animationAngle, _animationTime).OnComplete(() =>
        {
            _infoList.Clear();
        }));
    }

    private void SetPatternInfo()
    {
        int startAngle = 360 / _waveCount;
        for (int i = 0; i < _waveCount; ++i)
        {
            PATTERN10INFO info = new PATTERN10INFO();
            info.TweenAngle = startAngle * (i + 1);
            info.TweenAngleReverse = startAngle * (i + 1);
            info.CreateTime = 0.2f;

            _infoList.Add(info);
        }
    }
}
