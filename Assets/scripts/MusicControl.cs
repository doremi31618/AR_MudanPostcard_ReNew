using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour {
    /// <summary>
    /// 此腳本為聲音的GameManager
    /// </summary>

    [Header("需放入所有的語音導覽遊戲物件")]
    public List<AudioSource> audios;
    public bool isPlay = true;
    private void FixedUpdate()
    {
        if (isPlay != true)
            UI_ControlOnOrOff(isPlay);
    }

    //統一管理所有語音，開關開時重置語音時間並且暫停，開關關時停止語音
    public void UI_ControlOnOrOff(bool OnOrOff)
    {
        isPlay = OnOrOff;
        for (int i = 0; i < audios.Count;i++){
            if(OnOrOff){
                audios[i].PlayScheduled(0);
                audios[i].Pause();
            }else{
                audios[i].Stop();
            }
        }
    }

    public void AudioGuideTriggerOn(AudioSource aud)
    {
        if(isPlay)
        {
            Debug.Log("play music");
            aud.PlayScheduled(0);
        }

            
    }

    public void AudioGuideTriggerOff(AudioSource aud)
    {
        if (isPlay)
            aud.Stop();
    }

}
