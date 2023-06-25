using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //控制面板透明度
    private CanvasGroup canvasGroup;

    private float alphaSpeed = 10f;
    
    public bool isShow = false;

    private UnityAction hideCallBack = null;
    protected void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
    }

    // Start is called before the first frame update
   protected virtual void Start()
    {
        Init();
    }

   public virtual void ShowMe()
   {
       canvasGroup.alpha = 0;
       isShow = true;
   }

   public virtual void HideMe(UnityAction callBack)
   {
       canvasGroup.alpha = 1;
       isShow = false;
       hideCallBack = callBack;
   }
   
  //注册控件事件的方法，所有子面板都需要实现
   public abstract void Init();
   // Update is called once per frame
   protected virtual void Update()
    {
        //淡入
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += Time.deltaTime * alphaSpeed;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
            //淡出
        }else if (!isShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= Time.deltaTime * alphaSpeed;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                hideCallBack?.Invoke();
            }
        }
    }
}
