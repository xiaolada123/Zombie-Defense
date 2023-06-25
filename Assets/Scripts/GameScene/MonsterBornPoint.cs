using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterBornPoint : MonoBehaviour
{
    //最大波数
    public int maxWave;
    //每波怪物数
    public int monsterNumPerWave;
    //当前波还剩多少怪物
    private int nowNum;

    public List<int> monsterIDs;
    //当前波生成怪物ID
    private int nowID;
    //每只怪物生成间隔时间
    [Header("每只怪物生成间隔时间")]
    public float createOffsetTime;
    
    //波间间隔时间
    [Header("波间间隔时间")]
    public float delayTime;
    //第一波怪物生成等待时间
    [Header("第一波怪物生成等待时间")]
    public float firstDelayTime;

    private void CreateWave()
    {
        nowID = monsterIDs[Random.Range(0, monsterIDs.Count)];
        nowNum = monsterNumPerWave;
        CreateMonster();
        --maxWave;
        
        GameLevelMgr.Instance.SubtractNowWaveNum(1);
    }

    private void CreateMonster()
    {
        MonsterInfo info = GameDataMgr.Instance.monsterInfoList[nowID - 1];
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), 
            transform.position,Quaternion.identity);
        MonsterObject monsterObject = obj.AddComponent<MonsterObject>();
        monsterObject.InitInfo(info);

        --nowNum;
        GameLevelMgr.Instance.AddMonster(monsterObject);
        if (nowNum == 0)
        {
            if (maxWave > 0)
            {
                Invoke("CreateWave",delayTime);
            }
        }
        else
        {
            Invoke("CreateMonster",createOffsetTime);
        }
        
        
    }
    
    private void Start()
    {
        Invoke("CreateWave",firstDelayTime);
        GameLevelMgr.Instance.AddMonsterPoint(this);
        GameLevelMgr.Instance.UpdateMaxNum(maxWave);
    }

    public bool CheckOver()
    {
        return nowNum == 0 && maxWave == 0;
    }
}
