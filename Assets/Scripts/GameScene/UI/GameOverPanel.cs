using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Text txtTitle;
    public Text txtInfo;
    public Text txtCoinNum;
    public Button btnConfirm;
    
    public override void Init()
    {
        btnConfirm.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BeginScene");
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<GamePanel>();
            UIManager.Instance.ShowPanel<BeginPanel>();
            
            GameLevelMgr.Instance.ClearInfo();
        });
    }

    public void InitInfo(bool isWin,int coinNum)
    {
        if (isWin)
        {
            txtTitle.text = "You Win";
            txtInfo.text = "你成功保卫了基地";
        }
        else
        {
            txtTitle.text = "GameOver";
            txtInfo.text = "僵尸攻陷了基地";
        }

        txtCoinNum.text = (coinNum/10).ToString();

        //局内金币以10:1的比例转化为局外金币
        GameDataMgr.Instance.playerData.coinAmount += coinNum/10;
        GameDataMgr.Instance.SavePlayerData();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        Cursor.lockState = CursorLockMode.None;
    }
}
