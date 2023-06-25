using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;

    public Button btnUnlock;
    public Text txtUnlock;

    public Button btnStart;
    public Button btnBack;

    public Text txtMoney;
    public Text txtName;

    private Transform heroPos;

    //当前场景显示的对象
    private GameObject heroObj;
    //当前使用的数据
    private RoleInfo nowRoleData;
    //当前使用数据的索引
    private int nowIndex;
    public override void Init()
    {
        heroPos = GameObject.Find("HeroPos").transform;

        txtMoney.text = GameDataMgr.Instance.playerData.coinAmount.ToString();
        
        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
            {
                nowIndex = GameDataMgr.Instance.roleInfoList.Count - 1;
            }
            ChangeHero();
        });
        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex > GameDataMgr.Instance.roleInfoList.Count - 1)
            {
                nowIndex = 0;
            }
            ChangeHero();
        });
        btnStart.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.nowSelRole = nowRoleData;
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            UIManager.Instance.ShowPanel<ChooseScenePanel>();
        });
        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseHeroPanel>();
            Camera.main.GetComponent<CameraAnimator>().GoBack(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
        });
        //解锁按键触发
        btnUnlock.onClick.AddListener(() =>
        {
            PlayerData data = GameDataMgr.Instance.playerData;
            if (data.coinAmount >= nowRoleData.lockMoney)
            {
                data.coinAmount -= nowRoleData.lockMoney;
                txtMoney.text = data.coinAmount.ToString();
                
                data.buyHero.Add(nowRoleData.id);
                GameDataMgr.Instance.SavePlayerData();
                UpdateLockBtn();
                //提示面板购买成功
                UIManager.Instance.ShowPanel<TipPanel>().ChangeTip("购买成功");
            }
            else
            {
                //提示购买失败
                UIManager.Instance.ShowPanel<TipPanel>().ChangeTip("购买失败");
            }
        });
        ChangeHero();
    }

    private void ChangeHero()
    {
        if (heroObj != null)
        {
            Destroy(heroObj);
           // heroObj = null;
        }
        nowRoleData = GameDataMgr.Instance.roleInfoList[nowIndex];
        //实例化对象
        heroObj= Instantiate(Resources.Load<GameObject>(nowRoleData.res), heroPos.position, heroPos.rotation);
        
        Destroy(heroObj.GetComponent<PlayerObject>());
       txtName.text = nowRoleData.tips;
       UpdateLockBtn();
    }

    private void UpdateLockBtn()
    {
        if (nowRoleData.lockMoney > 0 && !GameDataMgr.Instance.playerData.buyHero.Contains(nowRoleData.id))
        {
            btnUnlock.gameObject.SetActive(true);
            txtUnlock.text = "解锁：" + nowRoleData.lockMoney;
            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnUnlock.gameObject.SetActive(false);
            btnStart.gameObject.SetActive(true);
        }
    }

    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        if (heroObj != null)
        {
            DestroyImmediate(heroObj);
           // heroObj = null;
        }
    }
}
