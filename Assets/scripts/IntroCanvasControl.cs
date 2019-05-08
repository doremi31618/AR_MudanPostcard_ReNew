using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IntroCanvasControl : MonoBehaviour {
    /// <summary>
    /// plaese check 
    /// 1.if there are "Canvas" tag
    /// 2. there are IntroCavas and have same name as below
    /// </summary>
    public postcard_index _index;

    //when it eaual to true,canvas no longer show up
    public bool IntroCanvasEnable = true;
    [Header("控制intro canvas開關用的")]
    public GameObject IntroCanvas;
    [Header("填入相同的介紹圖")]
    public GameObject _introCanvas;
    public bool isIntroButton = false;

    private void Start()
    {
        IntroCanvas.SetActive(IntroCanvas);
        if(!isIntroButton)
        {
            _introCanvas.SetActive(false);
        }

    }
    public void ClickIntroButtonControl(bool introActive)
    {
        IntroCanvasEnable = introActive;
        IntroCanvas.SetActive(introActive);
    }
    public void CheckIntroIndex(bool tracked)
    {
        //GameObject intro_now = _introCanvas;
        OnTracking(_introCanvas, tracked);
        /*
        switch(_index)
        {
            case postcard_index.AronYiAncientRoad :
                intro_now = GameObject.Find("Intro_AronYiAncientRoad");
                break;
            case postcard_index.AncientBattelField:
                intro_now = GameObject.Find("Intro_AncientBattelField");
                break;
            case postcard_index.CryingLake:
                intro_now = GameObject.Find("Intro_CryingLake");
                break;
            case postcard_index.MudanResevoir:
                break;
            case postcard_index.WaterGrassland:
                break;
        }*/

            
    }
    //voice play
    //show intro (if cavas is eanble)
    void OnTracking(GameObject indexName,bool isEnable)
    {
        indexName.SetActive(isEnable);
        if(isEnable)
        {
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }

    }

}
public enum postcard_index   
{
    AronYiAncientRoad,
    AncientBattelField,
    CryingLake,
    WaterGrassland,
    MudanResevoir,
    controller
}

