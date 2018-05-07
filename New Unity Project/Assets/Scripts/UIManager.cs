using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject objTitle = null;
    public GameObject objStartButton = null;
    public UILabel lbTimer = null;

    public AnimationCurve curve_MainUIAnim = null;

    private int _cnt = 3;
    private float _timerCnt = 1f;
    private bool _isTimer = false;

    private void Start()
    {
        lbTimer.gameObject.SetActive(false);
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
        objStartButton.transform.DOLocalMoveY(-530f, duration).SetEase(curve_MainUIAnim).SetDelay(duration).OnComplete(() =>
        {
            _isTimer = true;
        });
    }
}
