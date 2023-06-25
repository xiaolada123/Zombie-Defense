using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    //炮塔头部
    public Transform head;
    //开火点
    public Transform firePos;

    private float roundSpeed=20f;

    //炮塔数据结构类
    private TowerInfo info;
    private MonsterObject target;
    //用于计时实现攻击间隔
    private float atkTime;

    private Vector3 monsterPos;

    private List<MonsterObject> targets;
    public void InitInfo(TowerInfo info)
    {
        this.info = info;
    }

    private void Update()
    {
        if (info.atkType == 1)
        {
            //寻找攻击对象
            if (target == null || target.isDead ||
                Vector3.Distance(transform.position, target.transform.position) > info.atkRange)
            {
               target= GameLevelMgr.Instance.FindMonster(transform.position, info.atkRange);
            }

            if (target == null)
            {
                return;
            }
            //炮塔旋转
            monsterPos = target.transform.position;
            monsterPos.y = head.position.y;
            head.rotation=Quaternion.Lerp(head.rotation,Quaternion.LookRotation(monsterPos-head.position),
                roundSpeed*Time.deltaTime );

            //炮塔开火
            if (Vector3.Angle(head.forward, monsterPos - head.position) < 5&& Time.time-atkTime>info.intervalTime)
            {
                atkTime = Time.time;
                target.Wound(info.atk);
                GameDataMgr.Instance.PlayerSound("Music/Tower");
                GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff),
                    firePos.position,firePos.rotation);
                Destroy(effObj,0.2f);
            }
        }else if (info.atkType == 2)
        {
            //实现范围攻击
            targets = GameLevelMgr.Instance.FindMonsters(transform.position, info.atkRange);
            if (targets.Count > 0 &&Time.time-atkTime>info.intervalTime)
            {
                atkTime = Time.time;
                for (int i = 0; i < targets.Count; i++)
                {
                    targets[i].Wound(info.atk);
                    //播放音效和开火特效
                    GameDataMgr.Instance.PlayerSound("Music/Tower");
                    GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff),
                        transform.position,transform.rotation);
                    Destroy(effObj,0.2f);
                }
            }
        }
    }
}
