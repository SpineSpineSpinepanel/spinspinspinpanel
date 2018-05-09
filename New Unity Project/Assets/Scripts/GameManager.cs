using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public AudioSource audio_MainBGM;
    public Player player;
    public UISprite sprCircleBg;
    public GameObject objCircleBg;

    public GameObject endFXPrefab = null;

    private List<ParticleSystem> _listEndPop = new List<ParticleSystem>();

    public int MaxLevel = 3;
    public bool IsDie = false;
    public bool IsNextLevel = false;

    public ParticleSystem[] ParticleSystemArr;


    //[HideInInspector]
    public int CurrentPatternTotalBallNumber = 0;
    private int _currentPatternBallCnt = 0;
    private int _levelCnt = 0;

    //[HideInInspector]
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

    public void OnGameStart()
    {
        BulletPattenManager.GetInstance().CreateNewPattern();
        audio_MainBGM.Play();
    }

    public void StartNextLevel()
    {
        for (int i = 0; i < MaxLevel; ++i)
        {
            Invoke("levelAniamtion", 0.5f * i);
        }
    }

    private void levelAniamtion()
    {
        LevelCnt--;
        DOTween.To(() => sprCircleBg.fillAmount, x => sprCircleBg.fillAmount = x, (float)LevelCnt / (float)MaxLevel, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (LevelCnt == 0)
            {
                IsNextLevel = false;
                Invoke("StartNextPattern",0.5f);
            }

        });
        Debug.Log((float)LevelCnt / (float)MaxLevel);
        for (int i = 0; i < ParticleSystemArr.Length; ++i)
        {
            ParticleSystemArr[i].Stop();
            ParticleSystemArr[i].Play();
        }
    }

    private void StartNextPattern()
    {
        BulletPattenManager.GetInstance().CreateNewPattern();
        LevelCnt = 0;
    }

    public void SetLevelProgeress()
    {
        float FillAmount = 0f;
        if (_isTimeCheck)
        {
            if (_currentPatternTimeCount >= CurrentPatternTotalTime)
            {
                _isPattern = false;
                _currentPatternTimeCount = CurrentPatternTotalTime;
            }
            FillAmount = (((float)_currentPatternTimeCount / (float)CurrentPatternTotalTime) / (float)MaxLevel) + ((float)(LevelCnt - 1) / (float)MaxLevel);
        }
        else
        {
            if (_currentPatternBallCnt >= CurrentPatternTotalBallNumber)
            {
                _isPattern = false;
                _currentPatternBallCnt = CurrentPatternTotalBallNumber;
            }
            FillAmount = (((float)_currentPatternBallCnt / (float)CurrentPatternTotalBallNumber) / (float)MaxLevel) + ((float)(LevelCnt - 1) / (float)MaxLevel);
            _currentPatternBallCnt++;
        }

        DOTween.To(() => sprCircleBg.fillAmount, x => sprCircleBg.fillAmount = x, FillAmount, 0.1f).SetEase(Ease.Linear);
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
        particle.transform.localScale = new Vector3(95f, 95f, 1f);

        particle.Play();

        //yield return new WaitForSeconds(particle.main.duration);
        float elapsedTime = 0f;

        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (elapsedTime >= particle.main.duration)
                break;

            elapsedTime += Time.deltaTime;

            particle.transform.localScale = new Vector2(95f * objCircleBg.transform.localScale.x, 95f * objCircleBg.transform.localScale.y);
        }

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
