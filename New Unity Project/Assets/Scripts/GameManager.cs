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

<<<<<<< HEAD
    public GameObject endFXPrefab = null;
=======
    public int MaxLevel = 3;

>>>>>>> 5e62103f3f3d6b83b84e788722f617193181f055

    public int CurrentPatternTotalBallNumber = 0;
    private int _currentPatternBallCnt = 0;
    private int _levelCnt = 0;

    public float CurrentPatternTotalTime = 0f;
    private float _currentPatternTimeCount = 0f;
    private bool _isPattern = false;

    private bool _isTimeCheck = false;

    public float CurrentPatternTimeCount
    {
        get
        {
            return _currentPatternTimeCount;
        }

        set
        {
            _currentPatternTimeCount = value;
        }
    }

    public int LevelCnt
    {
        get
        {
            return _levelCnt;
        }

        set
        {
            _levelCnt = value;
        }
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isPattern && _isTimeCheck)
            _currentPatternTimeCount += Time.deltaTime;
    }

    public void SetLevelProgeress()
    {
        Debug.Log(_currentPatternTimeCount + " , " + CurrentPatternTotalTime);

        if (_isTimeCheck)
        {
            if (_currentPatternTimeCount >= CurrentPatternTotalTime)
            {
                _isPattern = false;
                _currentPatternTimeCount = CurrentPatternTotalTime;
            }

            sprCircleBg.fillAmount = ((float)_currentPatternTimeCount / (float)CurrentPatternTotalTime) * ((float)LevelCnt / (float)MaxLevel);
        }
        else
        {
            if (_currentPatternBallCnt >= CurrentPatternTotalBallNumber)
            {
                _isPattern = false;
                _currentPatternBallCnt = CurrentPatternTotalBallNumber;
            }

            sprCircleBg.fillAmount = ((float)_currentPatternBallCnt / (float)CurrentPatternTotalBallNumber) * ((float)LevelCnt / (float)MaxLevel);
            _currentPatternBallCnt++;
        }
    }

    public void SetBallCnt()
    {
        _currentPatternBallCnt = 0;
    }

    public void InitPatternStart(float TotalTime)
    {
        _currentPatternTimeCount = 0f;
        CurrentPatternTotalTime = TotalTime;
        _isPattern = true;
        _isTimeCheck = true;
    }

    public void InitPatternStart(int TotalCnt)
    {
        _currentPatternBallCnt = 0;
        CurrentPatternTotalBallNumber = TotalCnt;
        _isPattern = true;
        _isTimeCheck = false;
    }

    public void PlayBulletEndFX()
    {
        StartCoroutine(BulletEndFX());
    }

    private IEnumerator BulletEndFX()
    {
        var endFXObject = Instantiate(endFXPrefab, transform.parent);
        var particle = endFXObject.GetComponent<ParticleSystem>();

        particle.Play();

        yield return new WaitForSeconds(particle.main.duration);

        Destroy(endFXObject);
    }
}
