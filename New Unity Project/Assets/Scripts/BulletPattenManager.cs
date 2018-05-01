using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletPattenManager : MonoBehaviour
{
    IPattern CurPattern;

    public AnimationCurve curve_Angle;
    public AnimationCurve curve_Angle02;

    private float _patternTimeCount = 0f;
    // Use this for initialization
    void Start()
    {
        DOTween.useSmoothDeltaTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        _patternTimeCount += Time.deltaTime;

        if (_patternTimeCount >= GameManager.GetInstance().CurrentPatternTotalTime)
        {
            SetPattern(Random.Range(0, 4));
            _patternTimeCount = 0f;
            GameManager.GetInstance().LevelCnt++;
        }

        if (CurPattern != null)
        {
            //애니메이션이 실행중인가?
            if (CurPattern.IsTweening())
            {
                CurPattern.OnUpdate(Time.smoothDeltaTime);
            }
            else
            {
                CurPattern.OnEnd();
                //GameManager.GetInstance().SetBallCnt();
            }
        }
    }

    public void SetPattern(int pattern)
    {
        if(pattern == 0)
        {
            IPattern patten = new Pattern01(15, 10f, 1.5f, 180f, curve_Angle);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(11.5f);
        }

        if (pattern == 1)
        {
            IPattern patten = new Pattern02(6, 10f, 1.5f, 360f, curve_Angle02);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(11.5f);
        }

        if (pattern == 2)
        {
            //IPattern patten = new Pattern03(300, 10f, 0.05f);
            //patten.OnStart();
            //CurPattern = patten;
            //GameManager.GetInstance().InitPatternStart(300);

            IPattern patten = new Pattern04(15, 10f, 1.5f, 180f, curve_Angle);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(11.5f);
        }

        if (pattern == 3)
        {
            IPattern patten = new Pattern04(15, 10f, 1.5f, 180f, curve_Angle);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(11.5f);
        }
    }

    public int GetCurrentPatternBallCnt()
    {
        return CurPattern.GetTotalBallCount();
    }


}
