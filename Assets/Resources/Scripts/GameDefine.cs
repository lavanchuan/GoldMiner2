using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefine : MonoBehaviour
{
    public enum GameScreen { MENU, PLAY};
    public enum PlayerState { ROTATION, SHOOT, REWIND };
    public enum PlayState {PLAY, PAUSE, LOSE};

    // Item Object
    public static float maxSize = 0.5f;
    public static float minSize = 0.2f;
    public static float normalWeight = 1f;
    // GOLD
    public static float goldValue = 50f;
    public static float goldWeight = 1.5f * normalWeight;
    // ROCK
    public static float rockValue = 10f;
    public static float rockWeight = 2.5f * normalWeight;
    // DIAMOND
    public static float diamondValue = 800;
    public static float diamondWeight = 1.2f * normalWeight;
    
    // BAG
    public static float bagWeight = normalWeight;

    // TNT
    public static float tntValue = 2;
    public static float tntWeight = normalWeight;


    // TEXT NOTIFIC
    public static string CAUSE_OF_LOSE = "Đã hết thời gian và bạn đã không hoàn thành mục tiêu";
    public static string RESULT = "THUA";
    

}
