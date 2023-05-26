using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleXXX : MonoBehaviour
{
    public string Descript = "Bội thu, nhân 3 giá trị đồ vật";
    public float Price = 100;
    public static float PRICE_MIN = 1000;
    public static float PRICE_MAX = 3000;

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
