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
    public GameObject objCircleBg;

    public GameObject endFXPrefab = null;

    private List<ParticleSystem> _listEndPop = new List<ParticleSystem>();

    public int MaxLevel = 3;


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
        if (_isTimeCheck)
        {
            if (_currentPatternTimeCount >= CurrentPatternTotalTime)
            {
                _isPattern = false;
                _currentPatternTimeCount = CurrentPatternTotalTime;
            }

            sprCircleBg.fillAmount = (((float)_currentPatternTimeCount / (float)CurrentPatternTotalTime) / (float)MaxLevel) + ((float)(LevelCnt - 1) / (float)MaxLevel);
        }
        else
        {
            if (_currentPatternBallCnt >= CurrentPatternTotalBallNumber)
            {
                _isPattern = false;
                _currentPatternBallCnt = CurrentPatternTotalBallNumber;
            }

            sprCircleBg.fillAmount = (((float)_currentPatternBallCnt / (float)CurrentPatternTotalBallNumber) / (float)MaxLevel) + ((float)(LevelCnt - 1) / (float)MaxLevel);
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
        ParticleSystem particle = GetEndPop();

        particle.Play();

        yield return new WaitForSeconds(particle.main.duration);

        particle.gameObject.SetActive(false);
    }

    public void CreateEndPop()
    {
        for (int i = 0; i < 500; ++i)
        {
            GameObject obj = Instantiate(endFXPrefab);
            _listEndPop.Add(obj.GetComponent<ParticleSystem>());

            obj.name = "EndPop_" + i;
            obj.transform.parent = BulletManager.GetInstance().transform;
            obj.transform.localScale = new Vector3(95f, 95f, 1f);
            obj.transform.localPosition = new Vector3(0f, 0f, 0f);
            obj.gameObject.SetActive(false);
        }
    }

    public ParticleSystem GetEndPop()
    {
        for (int i = 0; i < _listEndPop.Count; ++i)
        {
            if (!_listEndPop[i].gameObject.activeSelf)
            {
                _listEndPop[i].gameObject.SetActive(true);
                return _listEndPop[i];
            }
        }

        CreateEndPop();

        for (int i = 0; i < _listEndPop.Count; ++i)
        {
            if (!_listEndPop[i].gameObject.activeSelf)
            {
                _listEndPop[i].gameObject.SetActive(true);
                return _listEndPop[i];
            }
        }

        return null;
    }
}
