using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMute : MonoBehaviour
{
    AudioSource audio;
    public static string music = "aud_menu";
    public static string touch_sound = "touch";
    public static string pickup_sound = "pickup";
    public static string buy_sound = "buyItem";
    public static string diamond_sound = "diamond";
    public static string tnt_sound = "tnt";
    public static string lose_sound = "lose";
    public static string win_sound = "win";
    public static string tick_sound = "tick";
    public static string shoot_sound = "shoot";
    public static string rewind_sound = "rewind";
    public bool isMuteMusic = false;
    public bool isMuteSound = false;

    void Start()
    {
       audio = (AudioSource) GetComponent<AudioSource>();
    }

    void Update()
    {
        audio.loop = true;

        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().mute = isMuteMusic;
        // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute = isMuteMusic;
    }

    public void PlayAudio(string fileName){
        audio.PlayOneShot((AudioClip)Resources.Load("Audio/" + fileName));
    }
}
