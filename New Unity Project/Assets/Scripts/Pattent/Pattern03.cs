using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern03 : IPattern
{
    public int _totalBallNumber;
    private float _animationTime;
    private float _createTime;

    private int _BallCnt = 0;
    private float _createTimeCount = 0f;

    private bool _tweening = true;
    public Pattern03(int TotalBallNumber, float AnimationTime, float CreateTime)
    {
        _totalBallNumber = TotalBallNumber;
        _animationTime = AnimationTime;
        _createTime = CreateTime;
    }

    public int GetTotalBallCount()
    {
        return _totalBallNumber;
    }

    public bool IsTweening()
    {
        return _tweening;
    }

    public void OnEnd()
    {

    }

    public void OnStart()
    {
    }

    public void OnUpdate(float deletaTime)
    {
        if (_BallCnt >= _totalBallNumber)
        {
            _tweening = false;
            return;
        }
        _createTimeCount += Time.deltaTime;

        if (_createTimeCount >= _createTime)
        {
            _createTime = Random.Range(0.0f, 0.1f);
            float Angle = Random.Range(0f, 360f);
            float BulletMoveTime = Random.Range(1.0f, 2.0f);

            BasicBullet temp = BulletManager.GetInstance().GetBullet();

            temp.OnActive(Angle, 730f, BulletMoveTime);
            _createTimeCount = 0f;
            _BallCnt++;
        }

    }
}
