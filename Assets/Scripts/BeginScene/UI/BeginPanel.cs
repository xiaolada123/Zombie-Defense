using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnAbout;
    public Button btnQuit;
    public Button btnHelp;
    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            Camera.main.GetComponent<CameraAnimator>().GoAhead(() =>
            {
                UIManager.Instance.ShowPanel<ChooseHeroPanel>();
            });
            UIManager.Instance.HidePanel<BeginPanel>();
        } );
        btnSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        } );
        btnAbout.onClick.AddListener(() =>
        {
           TipPanel panel= UIManager.Instance.ShowPanel<TipPanel>();
           panel.ChangeTip("作者：小拉达 时间：2023年6月");
        } );
        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        } );
        btnHelp.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<HelpPanel>();
        });
    }

}
