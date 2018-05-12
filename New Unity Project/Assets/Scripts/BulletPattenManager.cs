using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletPattenManager : MonoBehaviour
{
    private static BulletPattenManager instance;
    public static BulletPattenManager GetInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType(typeof(BulletPattenManager)) as BulletPattenManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    IPattern CurPattern;

    public AnimationCurve curve_Angle;
    public AnimationCurve curve_Angle02;
    public AnimationCurve curve_Angle03;
    public AnimationCurve curve_Angle04;

    private float _patternTimeCount = 0f;
    private int _prvePattern = -1;
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
            GameManager.GetInstance().StartNextLevel();
        }
        //return;
        _patternTimeCount += Time.deltaTime;


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

    public void CreateNewPattern()
    {
        if (!GameManager.GetInstance().IsDie && BulletManager.GetInstance().CheckBullet())
        {
            if (!GameManager.GetInstance().IsNextLevel)
            {
                if (GameManager.GetInstance().LevelCnt >= GameManager.GetInstance().MaxLevel)
                {
                    GameManager.GetInstance().IsNextLevel = true;
                    GameManager.GetInstance().StartNextLevel();
                    return;
                }
                int Pattern = Random.Range(0, 9);//9  <- For Test.

                while (Pattern == _prvePattern)
                {
                    Pattern = Random.Range(0, 9);
                }

                SetPattern(Pattern);
                _prvePattern = Pattern;
                _patternTimeCount = 0f;
                GameManager.GetInstance().LevelCnt++;
                Debug.Log(GameManager.GetInstance().LevelCnt);
            }
        }
    }

    public void SetPattern(int pattern)
    {
        if (pattern == 0)
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
            IPattern patten = new Pattern03(300, 10f, 0.05f);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(300);
        }

        if (pattern == 3)
        {
            IPattern patten = new Pattern04(15, 10f, 1.5f, 180f, curve_Angle);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(11.5f);
        }

        if (pattern == 4)
        {
            IPattern patten = new Pattern05(8, 10f, 1.5f, 270f, curve_Angle03);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(11.5f);
        }

        if (pattern == 5)
        {
            IPattern patten = new Pattern06(10, 10f, 1.5f, 360f, 0.25f, curve_Angle);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(10f + 10f * 0.25f);
        }

        if (pattern == 6)
        {
            IPattern patten = new Pattern07(1f, 1.5f, 25f, 0.25f, 15f, 8, curve_Angle04);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart((1f * 8f) + (2f * 0.25f));
        }

        if (pattern == 7)
        {
            IPattern patten = new Pattern08(3, 10f, 1.5f, 360f, 0.1f, 0.3f, curve_Angle);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(11.5f);
        }

        if (pattern == 8)
        {
            IPattern patten = new Pattern08(8, 10f, 1.5f, 270f, 0.3f, 0.3f, curve_Angle);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(11.5f);
        }

        if(pattern == 9)
        {
            IPattern patten = new Pattern09(10f, 1.5f, 360f*3);
            patten.OnStart();
            CurPattern = patten;
            GameManager.GetInstance().InitPatternStart(11.5f);
        }
    }
}
