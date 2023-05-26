using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRandom : MonoBehaviour
{

    public static bool HasObject()
    {

        if ((int)Random.Range(0, 1000) % 2 == 0)
            return true;
        return false;
    }

    public static float GetPrice(int min, int max)
    {
        int result = (int)Random.Range(min, max);
        return (float)result;
    }
}
