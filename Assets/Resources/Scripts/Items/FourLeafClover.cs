using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourLeafClover : MonoBehaviour
{
    public string Descript = "Nhận được các đồ vật có giá trị hơn trong các túi đồ";
    public float Price;
    public static float PRICE_MIN = 200;
    public static float PRICE_MAX = 1500;

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
