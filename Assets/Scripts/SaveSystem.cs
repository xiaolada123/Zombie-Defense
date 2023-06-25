using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public static class SaveSystem
{
    // #region PlayerPrefs
    // public static void SaveByPlayerPrefs(string key, object data)
    // {
    //     var json = JsonMapper.ToJson(data);
    //     PlayerPrefs.SetString(key,json);
    //     PlayerPrefs.Save();
    // }
    //
    // public static string LoadFromPlayerPrefs(string key)
    // {
    //     return PlayerPrefs.GetString(key, null);
    // }
    //
    //
    // #endregion

    
    public static void SaveByJson(string saveFileName, object data)
    {
       // var json = JsonMapper.ToJson(data);
       var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, saveFileName+".json");
        
        File.WriteAllText(path,json);
        Debug.Log($"Successfully saved data to {path}.");

    }
    
    public static T LoadFromJson<T>(string saveFileName) where T:new()
    {
        string path = Path.Combine(Application.streamingAssetsPath, saveFileName+".json");
        if (!File.Exists(path))
        {
            path = Path.Combine(Application.persistentDataPath, saveFileName+".json");
        }
        if (!File.Exists(path))
        {
            return new T();
        }
        var json = File.ReadAllText(path);
       T data = JsonMapper.ToObject<T>(json);
        return data;
    }
    
    public static void DeleteSaveFile(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName+".json");
        File.Delete(path);
    }
}
