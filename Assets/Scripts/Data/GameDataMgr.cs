using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance=>instance;
    public MusicData musicData;
    public List<RoleInfo> roleInfoList;
    public PlayerData playerData;
    public List<SceneInfo> sceneInfoList;
    public RoleInfo nowSelRole;
    public List<MonsterInfo> monsterInfoList;

    public List<TowerInfo> towerInfoList;
    private GameDataMgr()
    {
        //读取json文件 musicData
       musicData= SaveSystem.LoadFromJson<MusicData>("MusicData");
       roleInfoList = SaveSystem.LoadFromJson<List<RoleInfo>>("RoleInfo");
       playerData = SaveSystem.LoadFromJson<PlayerData>("PlayerData");
       sceneInfoList = SaveSystem.LoadFromJson<List<SceneInfo>>("SceneInfo");
       monsterInfoList = SaveSystem.LoadFromJson<List<MonsterInfo>>("MonsterInfo");
       towerInfoList = SaveSystem.LoadFromJson<List<TowerInfo>>("TowerInfo");
    }

    public void SaveMusicData()
    {
        //保存json文件
        SaveSystem.SaveByJson("MusicData",musicData);
    }

    public void SavePlayerData()
    {
        SaveSystem.SaveByJson("PlayerData",playerData);
    }

    /// <summary>
    /// 播放音效方法
    /// </summary>
    /// <param name="resName"></param>
    public void PlayerSound(string resName)
    {
        GameObject musicObj = new GameObject();
        AudioSource a = musicObj.AddComponent<AudioSource>();
        a.clip = Resources.Load<AudioClip>(resName);
        a.volume = musicData.soundValue;
        a.mute = !musicData.soundOpen;
        a.Play();
        GameObject.Destroy(musicObj,1);
    }
}
