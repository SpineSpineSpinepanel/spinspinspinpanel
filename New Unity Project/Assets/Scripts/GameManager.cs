using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager GetInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }

    public UISprite sprCircleBg;


    public int CurrentPatternTotalBallNumber = 0;
    private int _currentPatternBallCnt = 0;
    private int _levelCnt = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLevelProgeress()
    {
        sprCircleBg.fillAmount = (float)_currentPatternBallCnt / (float)CurrentPatternTotalBallNumber;
        _currentPatternBallCnt++;
        Debug.Log("1 : " + _currentPatternBallCnt);
    }

    public void SetBallCnt()
    {
        _currentPatternBallCnt = 0;
    }
}
