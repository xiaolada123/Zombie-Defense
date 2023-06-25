using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound;
    public override void Init()
    {
        MusicData data = GameDataMgr.Instance.musicData;
        togMusic.isOn = data.musicOpen;
        togSound.isOn = data.soundOpen;

        sliderMusic.value = data.musicValue;
        sliderSound.value = data.soundValue;
        
        //隐藏面板
        btnClose.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.SaveMusicData();
            UIManager.Instance.HidePanel<SettingPanel>();
            Debug.Log("close");
        });
        togMusic.onValueChanged.AddListener((v) =>
        {
            BKMusic.Instance.SetIsOpen(v);
            GameDataMgr.Instance.musicData.musicOpen = v;
        });
        togSound.onValueChanged.AddListener((v) =>
        {
            GameDataMgr.Instance.musicData.soundOpen = v;
        });
        sliderMusic.onValueChanged.AddListener((v) =>
        {
            BKMusic.Instance.ChangeValue(v);
            GameDataMgr.Instance.musicData.musicValue = v;
        });
        sliderSound.onValueChanged.AddListener((v) =>
        {
            GameDataMgr.Instance.musicData.soundValue = v;
        });
    }
}
