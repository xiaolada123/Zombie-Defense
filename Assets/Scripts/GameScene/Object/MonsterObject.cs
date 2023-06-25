using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    private Animator animator;

    private NavMeshAgent agent;

    private MonsterInfo monsterInfo;
    //怪物当前血量
    private int hp;

    public bool isDead = false;

    private float lastTime = 0;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        hp = info.hp;

        agent.speed = agent.acceleration = info.moveSpeed;
        agent.angularSpeed = info.roundSpeed;
    }

    public void Wound(int dmg)
    {
        if (isDead)
        {
            return;
        }
        hp-=dmg;
        animator.SetTrigger("Wound");
        if (hp <= 0)
        {
            Dead();
        }
        else
        {
            GameDataMgr.Instance.PlayerSound("Music/Wound");
        }
    }

    public void Dead()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("Dead",true);
        GameLevelMgr.Instance.player.AddMoney(20);
    }

    public void DeadEvent()
    {
        GameDataMgr.Instance.PlayerSound("Music/Dead");
        GameLevelMgr.Instance.SubtractMonster(this);
        Destroy(gameObject,5);
        //每次怪物死亡时检查游戏是否胜利
        if (GameLevelMgr.Instance.CheckOver())
        {
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo(true,GameLevelMgr.Instance.player.coin);
            
        }
        
    }

    public void BornOver()
    {
        agent.SetDestination(MainBaseObject.Instance.transform.position);
        animator.SetBool("Run",true);
    }

    public void AtkEvent()
    {
        GameDataMgr.Instance.PlayerSound("Music/Eat");
       Collider[] colliders= Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1,
            1<<LayerMask.NameToLayer("MainBase"));
       for (int i = 0; i < colliders.Length; i++)
       {
           if (MainBaseObject.Instance.gameObject == colliders[i].gameObject)
           {
               MainBaseObject.Instance.Wound(monsterInfo.atk);
           }
       }
    }
    private void Update()
    {
        if (isDead)
        {
            return;
        }
        animator.SetBool("Run",agent.velocity!=Vector3.zero);
        if (Vector3.Distance(this.transform.position, MainBaseObject.Instance.transform.position) < 5 &&
            (Time.time - lastTime > monsterInfo.atkInterval))
        {
            animator.SetTrigger("Atk");
            lastTime = Time.time;
        }
    }
}
