using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GooglePlayGames;

public class UIManager : MonoBehaviour
{
    public GameObject objTitle = null;
    public GameObject objButton = null;
    public UILabel lbTimer = null;

    public AnimationCurve curve_MainUIAnim = null;

    private int _cnt = 3;
    private float _timerCnt = 1f;
    private bool _isTimer = false;

    private void Start()
    {
        lbTimer.gameObject.SetActive(false);


#if UNITY_ANDRIOD
        
#endif
    }

    public void ClickRankBtn()
    {
        // 로그인이 되어 있지 않으면 로그인하고 보여주기
        if(Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if(success)
                {
                    Social.ShowAchievementsUI();
                    return;
                }
                else
                {
                    return;
                }
            });
        }

        Social.ShowAchievementsUI();
    }

    public void ReportScroe(int Score)
    {
        PlayGamesPlatform.Instance.ReportScore(Score, GPGSIds.leaderboard_cp, (bool success) =>
          {
              if(success)
              {
                  Debug.Log("success");
              }
              else
              {
                  Debug.Log("Fail");
              }
          });
    }

    private void Update()
    {
        if (_isTimer)
        {
            _timerCnt += Time.deltaTime;
            if (_timerCnt >= 1f)
            {
                _timerCnt = 0f;
                updateTimer();
            }
        }
    }

    private void updateTimer()
    {
        lbTimer.gameObject.SetActive(true);
        lbTimer.alpha = 0f;
        lbTimer.transform.localScale = new Vector3(0f, 0f, 1f);

        if (_cnt != 0)
            lbTimer.text = "" + _cnt;
        else
            lbTimer.text = "Start~!";

        Debug.Log(_cnt);

        lbTimer.transform.DOScale(1f, 1f);
        DOTween.ToAlpha(() => lbTimer.color, x => lbTimer.color = x, 1f, 1f).OnComplete(() =>
        {
            if (_cnt == -1)
            {
                _isTimer = false;
                gamestart();
            }
        });
        _cnt--;
    }

    private void gamestart()
    {
        lbTimer.alpha = 1f;

        lbTimer.transform.DOScale(0f, 1f);
        DOTween.ToAlpha(() => lbTimer.color, x => lbTimer.color = x, 0f, 1f).OnComplete(() =>
        {
            GameManager.GetInstance().OnGameStart();
        });

    }


    public void StartButtonAction()
    {
        float duration = 0.5f;

        objTitle.transform.DOLocalMoveY(500f, duration).SetEase(curve_MainUIAnim);
        objButton.transform.DOLocalMoveY(-270f, duration).SetEase(curve_MainUIAnim).SetDelay(duration).OnComplete(() =>
        {
            _isTimer = true;
        });
    }
}
