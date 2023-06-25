using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : BasePanel
{
    public Button btnConfime;
    public override void Init()
    {
    
        btnConfime.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<HelpPanel>();
        });
    }
}
