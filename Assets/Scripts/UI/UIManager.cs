using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();

    public static UIManager Instance => instance;

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    //场景中的canvas对象，用于设置为面板的父对象
    private Transform canvaTrans;

    private UIManager()
    {
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvaTrans = canvas.transform;
        
        GameObject.DontDestroyOnLoad(canvas);
    }
    
    //显示面板
    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        //判断字典中是否已经显示了面板
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvaTrans,false);

        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName,panel);
        panel.ShowMe();

        return panel;
    }
    
    //删除面板
    public void HidePanel<T>(bool isFade =true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }
    //得到面板
    public T GetPanel<T>() where T:BasePanel
    {
        if (panelDic.ContainsKey(typeof(T).Name))
        {
            return panelDic[typeof(T).Name] as T;
        }
        else
        {
            return null;
        }
    }
}
