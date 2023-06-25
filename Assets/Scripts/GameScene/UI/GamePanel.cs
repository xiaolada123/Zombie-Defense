using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel:BasePanel
{
    public Image imgLife;
    public Text txtBaseLife;
    public Text txtLeftWaveNum;
    public Text txtCoin;

    public float hpW = 600;
    public Button btnQuit;
    public Transform towersTrans;

    public List<TowerBtn> towersBtns = new List<TowerBtn>();

    private TowerPoint nowSelTowerPoint;

    private bool checkInput;
    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });
        
        towersTrans.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateTowerHP(int hp, int maxHP)
    {
        txtBaseLife.text = hp + "/" + maxHP;
        (imgLife.transform as RectTransform).sizeDelta = new Vector2(((float)hp / maxHP)*hpW, 50);
    }

    public void UpdateWaveNum(int nowNum,int maxNum)
    {
        txtLeftWaveNum.text = nowNum + "/" + maxNum;
    }

    public void UpdateCoin(int coin)
    {
        txtCoin.text = coin.ToString();
    }

    public void UpdateSelTower(TowerPoint point)
    {
        //根据造塔点信息显示内容
        nowSelTowerPoint = point;
        //如果离开造塔点，传入null，关闭造塔页面
        if (nowSelTowerPoint == null)
        {
            towersTrans.gameObject.SetActive(false);
            checkInput = false;
        }
        else
        {
            checkInput = true;
            towersTrans.gameObject.SetActive(true);
                //如果当前点没有塔，显示造塔页面
            if (nowSelTowerPoint.nowTowerInfo == null)
            {
                for (int i = 0; i < towersBtns.Count; i++)
                {
                    towersBtns[i].gameObject.SetActive(true);
                    towersBtns[i].InitInfo(nowSelTowerPoint.chooseIDs[i],"数字键"+(i+1));
                }
            }
            else
            {
                //如果当前点有塔，显示升级页面
                for (int i = 0; i < towersBtns.Count; i++)
                {
                    towersBtns[i].gameObject.SetActive(false);
                }
                towersBtns[1].gameObject.SetActive(true);
                towersBtns[1].InitInfo(nowSelTowerPoint.nowTowerInfo.nextLev,"空格键升级");
            }
        }
        
        
        
    }

    protected override void Update()
    {
        base.Update();

        if (!checkInput)
        {
            return;
        }
        
        if (nowSelTowerPoint.nowTowerInfo == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[0]);
            }else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[1]);
            }else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[2]);
            }else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIDs[3]);
            }
            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowTowerInfo.nextLev);
            }
        }
        
    }
}
