using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    public Text txtTip;
    public Text txtName;
    public Text txtCost;

    public void InitInfo(int id, string inputStr)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfoList[id - 1];
        txtName.text = info.name;
        txtCost.text = info.money.ToString();
        txtTip.text = inputStr;
        if (info.money > GameLevelMgr.Instance.player.coin)
        {
            txtCost.color=Color.red;
        }
        else
        {
            txtCost.color=Color.white;
        }
        
    }
}
