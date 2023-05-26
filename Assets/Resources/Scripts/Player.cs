using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;
    public float money;
    public float target;
    // property for items
    public float strength = 1f;
    public static float STRENGTH_DEFAULT = 1f;
    public static float X_VALUE_DIAMOND_DEFAULT = 1f;
    public float xValueDiamond;
    public bool onX3Value;
    public int timeAdd;
    public int fireCreackerQuantity;
    public bool useFourLeafClover = false;
    //
    public int timeSecond;
    int timeWarning = 5;
    public static int TIME_DEFAULT = 60;
    // check time
    public bool playing;

    public void Awake()
    {
        strength = 1f;
        onX3Value = false;
        xValueDiamond = X_VALUE_DIAMOND_DEFAULT;
        timeAdd = 0;
        fireCreackerQuantity = 0;
        useFourLeafClover = false;

        level = 0;
        money = 0;
        target = 0;
        playing = false;
        timeSecond = TIME_DEFAULT;
    }

    public int GetTimeSecond()
    {
        return timeSecond + timeAdd;
    }

    public void SetTimeSecond(int timeSecond)
    {
        this.timeSecond = timeSecond;
    }

    public void SetTimeSecond()
    {
        if (!GameObject.FindGameObjectWithTag("BtnPause").GetComponent<BtnPause>().isPause)
        {
            this.timeSecond -= 1;
            if(this.timeSecond <= timeWarning){
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMute>()
                .PlayAudio(AudioMute.tick_sound);
            }
        }
    }
}
