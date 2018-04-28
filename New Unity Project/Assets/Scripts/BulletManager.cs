using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private static BulletManager instance;
    public static BulletManager GetInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType(typeof(BulletManager)) as BulletManager;
            if (!instance)
                Debug.LogError("There needs to be one active MyClass script on a GameObject in your scene.");
        }

        return instance;
    }


    public List<BasicBullet> BulletList = new List<BasicBullet>();

    public GameObject pf_BasicBullet;
    // Use this for initialization
    void Start()
    {
        CreateBullet();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BulletList.Count; ++i)
        {
            //if (BulletList[i].gameObject.activeSelf)
            //    BulletList[i].OnUpdate();
        }
    }

    public void CreateBullet()
    {
        for (int i = 0; i < 500; ++i)
        {
            GameObject obj = Instantiate(pf_BasicBullet);
            BulletList.Add(obj.GetComponent<BasicBullet>());

            obj.name = "Bullet_" + i;
            obj.transform.parent = transform;
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            obj.transform.localPosition = new Vector3(5000f, 5000f, 0f);
            obj.gameObject.SetActive(false);
        }
    }

    public BasicBullet GetBullet()
    {
        for (int i = 0; i < BulletList.Count; ++i)
        {
            if (!BulletList[i].IsMoveing)
            {
                BulletList[i].IsMoveing = true;
                return BulletList[i];
            }
        }

        CreateBullet();

        for (int i = 0; i < BulletList.Count; ++i)
        {
            if (!BulletList[i].IsMoveing)
            {

                BulletList[i].IsMoveing = true;
                return BulletList[i];
            }
        }

        return null;

    }
}
