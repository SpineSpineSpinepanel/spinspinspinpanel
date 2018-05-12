using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using GooglePlayGames;

public class UIManager : MonoBehaviour
{
    public GameObject objTitle = null;
    public GameObject objButton = null;
    public GameObject objSettingBg;
    public UILabel lbTimer = null;
    public AudioSource source;
    public Camera camera;

    public AnimationCurve curve_MainUIAnim = null;

    private int _cnt = 3;
    private float _timerCnt = 1f;
    private bool _isTimer = false;

    private bool _isSetting = false;
    private bool _isGameStart = false;

    private void Start()
    {
        Screen.SetResolution(1280, 720, true);

        lbTimer.gameObject.SetActive(false);


#if UNITY_ANDRIOD
        
#endif
    }

    //public void ClickRankBtn()
    //{
    //    // 로그인이 되어 있지 않으면 로그인하고 보여주기
    //    if(Social.localUser.authenticated == false)
    //    {
    //        Social.localUser.Authenticate((bool success) =>
    //        {
    //            if(success)
    //            {
    //                Social.ShowAchievementsUI();
    //                return;
    //            }
    //            else
    //            {
    //                return;
    //            }
    //        });
    //    }

    //    Social.ShowAchievementsUI();
    //}

    //public void ReportScroe(int Score)
    //{
    //    PlayGamesPlatform.Instance.ReportScore(Score, GPGSIds.leaderboard_cp, (bool success) =>
    //      {
    //          if(success)
    //          {
    //              Debug.Log("success");
    //          }
    //          else
    //          {
    //              Debug.Log("Fail");
    //          }
    //      });
    //}

    private void Update()
    {
        if (_isTimer)
        {
            _timerCnt += Time.unscaledDeltaTime;
            if (_timerCnt >= 1f)
            {
                _timerCnt = 0f;
                updateTimer();
            }
        }
    }

    private void updateTimer()
    {
        if (_cnt <= -1) return;

        lbTimer.DOComplete();
        lbTimer.gameObject.SetActive(true);

        lbTimer.alpha = 0f;
        lbTimer.transform.localScale = new Vector3(0f, 0f, 1f);

        if (_cnt != 0)
            lbTimer.text = "" + _cnt;
        else
            lbTimer.text = "Start";

        lbTimer.transform.DOScale(1f, 1f).SetUpdate(true);
        DOTween.ToAlpha(() => lbTimer.color, x => lbTimer.color = x, 1f, 1f).OnComplete(() =>
        {

            if (!_isGameStart)
            {
                if (_cnt == -1)
                {
                    _isTimer = false;
                    _timerCnt = -10f;
                    gamestart();
                }
            }
            else if (_isGameStart && _isSetting)
            {
                if (_cnt == -1)
                {
                    _isSetting = false;
                    _isTimer = false;
                    _timerCnt = -10f;
                    regamestart();
                }
            }

        }).SetUpdate(true);

        _cnt--;
    }

    private void gamestart()
    {
        lbTimer.alpha = 1f;

        lbTimer.transform.DOScale(0f, 1f);
        DOTween.ToAlpha(() => lbTimer.color, x => lbTimer.color = x, 0f, 1f).OnComplete(() =>
        {
            GameManager.GetInstance().OnGameStart();
            _isGameStart = true;
        });
    }

    private void regamestart()
    {
        lbTimer.alpha = 1f;

        lbTimer.transform.DOScale(0f, 1f).SetUpdate(true);
        DOTween.ToAlpha(() => lbTimer.color, x => lbTimer.color = x, 0f, 1f).OnComplete(() =>
        {
            source.Play();
            Time.timeScale = 1f;
        }).SetUpdate(true);
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

    public void ClickSettingOn()
    {
        Time.timeScale = 0f;
        objSettingBg.gameObject.SetActive(true);
        source.Pause();
    }

    public void ClickSettingOff()
    {
        _isSetting = true;
        objSettingBg.gameObject.SetActive(false);

        if (!_isGameStart)
        {
            Time.timeScale = 1f;
        }
        else
        {
            _cnt = 3;
            _timerCnt = 5f;
            _isTimer = true;
        }
    }
}
