using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreacker : MonoBehaviour
{
    public string Descript = "Sử dụng để làm nổ các đồ vật đang kéo lên";
    public float Price = 100;
    public static float PRICE_MIN = 50;
    public static float PRICE_MAX = 1000;

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
            Price = Random.Range(PRICE_MIN, PRICE_MAX);
        }
    }
}
