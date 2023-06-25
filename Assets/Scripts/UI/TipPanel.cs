using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel:BasePanel
{
    public Text txtInfo;

    public Button btnConfirm;

    public override void Init()
    {
        btnConfirm.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    public void ChangeTip(string tip)
    {
        txtInfo.text = tip;
    }
}
