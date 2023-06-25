using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private int atk;

    public int coin;

    private float roundSpeed = 100f;

    private Animator anim;

    public Transform firePos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void InitPlayerInfo(int atk, int coin)
    {
        this.atk = atk;
        this.coin = coin;
        UpdateMoney();
    }
    
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("VSpeed",Input.GetAxis("Vertical"));
        anim.SetFloat("HSpeed",Input.GetAxis("Horizontal"));
        transform.Rotate(Vector3.up,Input.GetAxis("Mouse X")*roundSpeed*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetLayerWeight(1,1);
        }else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetLayerWeight(1,0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("Roll");
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Fire");
        }
    }

    public void KnifeEvent()
    {
        GameDataMgr.Instance.PlayerSound("Music/Knife");
       Collider[] colliders= Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1,
            1 << LayerMask.NameToLayer("Monster"));
       for (int i = 0; i < colliders.Length; i++)
       {
           //得到碰撞到的物体上的怪物脚本，让其受伤
          MonsterObject monsterObj= colliders[i].GetComponent<MonsterObject>();
          if (monsterObj != null&& !monsterObj.isDead)
          {
              monsterObj.Wound(atk);
          }
       }
    }

    public void ShootEvent()
    {
        GameDataMgr.Instance.PlayerSound("Music/Gun");
        Debug.Log("Shoot");
        RaycastHit[] hits = Physics.RaycastAll(new Ray(firePos.position, transform.forward), 1000,
            1 << LayerMask.NameToLayer("Monster"));
        for (int i = 0; i < hits.Length; i++)
        {
            //得到碰撞到的物体上的怪物脚本，让其受伤
            MonsterObject monsterObj= hits[i].collider.GetComponent<MonsterObject>();
            if (monsterObj != null&& !monsterObj.isDead)
            {
                GameObject effObj = Instantiate(Resources.Load<GameObject>(GameDataMgr.Instance.nowSelRole.hitEff));
                effObj.transform.position=hits[i].point;
                effObj.transform.rotation=Quaternion.LookRotation(hits[i].normal);
                Destroy(effObj,1);
                monsterObj.Wound(atk);
            }
        }
    }

    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateCoin(coin);
    }

    public void AddMoney(int money)
    {
        coin += money;
        UpdateMoney();
    }
}
