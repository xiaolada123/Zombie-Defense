using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBaseObject : MonoBehaviour
{
    private int hp;

    private int maxHP;

    private bool isDead=false;

    private static MainBaseObject instance;

    public static MainBaseObject Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHP(int hp, int maxHP)
    {
        this.hp =hp;
        this.maxHP = maxHP;
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHP(hp,maxHP);
    }

    public void Wound(int dmg)
    {
        if (isDead)
        {
            return;
        }

        hp -= dmg;
        //如果hp降到0以下，游戏失败
        if (hp <= 0)
        {
            isDead = true;
            hp = 0;
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo(false,GameLevelMgr.Instance.player.coin);
        }
        UpdateHP(hp,maxHP);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
