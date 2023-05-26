using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle2 : MonoBehaviour
{
    public string Descript = "Các viên kim cương có giá trị hơn";
    public float Price = 100;
    public static float PRICE_DEFAULT = 500;
    public static float PRICE_MAX = 2000;
    float xValue = 1f;
    float minXValue = 1.5f;
    float maxXValue = 2f;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level == 0)
        {
            Destroy(gameObject);
        }

        if (!MyRandom.HasObject() )
            Destroy(gameObject);
        else
        {
            Price = Random.Range(PRICE_DEFAULT, PRICE_MAX);
            xValue = Random.Range(minXValue, maxXValue);
        }
    }

    public float GetXValue(){
        return this.xValue;
    }
}
