using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletPattenManager : MonoBehaviour
{
    IPattern CurPattern;

    public AnimationCurve curve_Angle;
    public AnimationCurve curve_Angle02;
    // Use this for initialization
    void Start()
    {
        DOTween.useSmoothDeltaTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            IPattern patten = new Pattern01(15, 10f, 1.5f, 180f, curve_Angle);
            patten.OnStart();
            CurPattern = patten;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            IPattern patten = new Pattern02(6, 10f, 1.5f, 360f, curve_Angle02);
            patten.OnStart();
            CurPattern = patten;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            IPattern patten = new Pattern03(300, 10f, 0.05f);
            patten.OnStart();
            CurPattern = patten;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            IPattern patten = new Pattern04(15, 10f, 1.5f, 180f, curve_Angle);
            patten.OnStart();
            CurPattern = patten;
        }

        if (CurPattern != null)
        {
            //애니메이션이 실행중인가?
            if (CurPattern.IsTweening())
            {
                CurPattern.OnUpdate(Time.smoothDeltaTime);
                GameManager.GetInstance().CurrentPatternTotalBallNumber = CurPattern.GetTotalBallCount();
            }
            else
            {
                CurPattern.OnEnd();
                //GameManager.GetInstance().SetBallCnt();
            }
        }

    }

    public int GetCurrentPatternBallCnt()
    {
        return CurPattern.GetTotalBallCount();
    }


}
