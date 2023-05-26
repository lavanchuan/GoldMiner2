using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public string Descript = "Thời gian là vàng là bạc";
    public float Price = 100;
    public static float PRICE_DEFAULT = 100;
    public static float PRICE_MAX = 2000;
    int timeAdd;
    public static int TIME_ADD_MIN = 5;
    public static int TIME_ADD_MAX = 30;

    private void Awake()
    {

        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level == 0)
        {
            Destroy(gameObject);
        }
        if (!MyRandom.HasObject())
            Destroy(gameObject);
        else
        {
            Price = Random.Range(PRICE_DEFAULT, PRICE_MAX);
            timeAdd = Random.Range(TIME_ADD_MIN, TIME_ADD_MAX);
        }
    }

    public int GetTimeAdd(){
        return this.timeAdd;
    }
}
