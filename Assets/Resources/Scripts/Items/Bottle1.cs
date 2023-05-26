using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle1 : MonoBehaviour
{
    public string Descript = "Thể lực tăng mạnh, sức khỏe dồi dào";
    public float Price;
    private const float PRICE_DEFAULT = 100;
    private const float PRICE_MAX = 2000;
    public float strength = 2f;

    private void Awake() {
        Price = PRICE_DEFAULT;

        if(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>().level == 0){
            Destroy(gameObject);
        }
        
        if(!MyRandom.HasObject())
            Destroy(gameObject);
        else {
            Price = Random.Range(PRICE_DEFAULT, PRICE_MAX);
        }
    }
}
