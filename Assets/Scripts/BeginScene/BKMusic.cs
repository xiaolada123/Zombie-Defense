using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;

    public static BKMusic Instance => instance;

    private AudioSource bkSource;
    void Awake()
    {
        instance = this;
        bkSource = this.GetComponent<AudioSource>();

        MusicData data = GameDataMgr.Instance.musicData;
        
        SetIsOpen(data.musicOpen);
        ChangeValue(data.musicValue);
    }

    public void SetIsOpen(bool isOpen)
    {
        bkSource.mute = !isOpen;
    }

    public void ChangeValue(float v)
    {
        bkSource.volume = v;
    }
}
