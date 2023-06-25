using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();
    public static GameLevelMgr Instance => instance;

    public PlayerObject player;

    private List<MonsterBornPoint> points = new List<MonsterBornPoint>();

    private List<MonsterObject> monsterList = new List<MonsterObject>();

    //当前还有多少波
    private int nowWaveNum = 0;

    //一共有多少波
    private int maxWaveNum = 0;
    

    private GameLevelMgr()
    {

    }

    //1、切换到场景时创建玩家
    public void InitInfo(SceneInfo info)
    {
        UIManager.Instance.ShowPanel<GamePanel>();

        RoleInfo roleInfo = GameDataMgr.Instance.nowSelRole;

        Transform heroPos = GameObject.Find("HeroBornPos").transform;

        GameObject heroObj =
            GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), heroPos.position, heroPos.rotation);

        player = heroObj.GetComponent<PlayerObject>();
        player.InitPlayerInfo(roleInfo.atk, 1000);
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);

        MainBaseObject.Instance.UpdateHP(300, 300);
    }
    //2、判断游戏是否胜利

    public void AddMonsterPoint(MonsterBornPoint point)
    {
        points.Add(point);
    }

    public bool CheckOver()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!points[i].CheckOver())
            {
                return false;
            }
        }

        if (monsterList.Count > 0)
        {
            return false;
        }
        
        
        return true;
    }

    public void UpdateMaxNum(int num)
    {
        maxWaveNum += num;
        nowWaveNum = maxWaveNum;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }

    public void SubtractNowWaveNum(int num)
    {
        nowWaveNum -= num;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }

    public void AddMonster(MonsterObject monsterObj)
    {
        monsterList.Add(monsterObj);
    }

    public void SubtractMonster(MonsterObject monsterObj)
    {
        monsterList.Remove(monsterObj);
    }

    //寻找单体怪物
    public MonsterObject FindMonster(Vector3 pos, int range)
    {
        for (int i = 0; i < monsterList.Count; i++)
        {

            if (!monsterList[i].isDead && Vector3.Distance(monsterList[i].transform.position, pos) <= range)
            {
                return monsterList[i];
            }
        }

        return null;
    }
    
    //寻找范围内所有怪物，并返回列表
    public List< MonsterObject> FindMonsters(Vector3 pos, int range)
    {
        List<MonsterObject> list = new List<MonsterObject>();
        for (int i = 0; i < monsterList.Count; i++)
        {

            if (!monsterList[i].isDead && Vector3.Distance(monsterList[i].transform.position, pos) <= range)
            {
                list.Add(monsterList[i]);
            }
        }

        return list;
    }
    
    //清空当前关卡数据，避免单例模式的数据保存到下一次关卡
    public void ClearInfo()
    {
        points.Clear();
        monsterList.Clear();
        player = null;
    }
}
