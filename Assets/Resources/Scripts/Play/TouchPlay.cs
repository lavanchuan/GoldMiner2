using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlay : MonoBehaviour
{
    GameObject touchPlay;

    void Update()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            switch(touch.phase){
                case TouchPhase.Began:
                touchPlay = (GameObject) Instantiate(Resources.Load("Prefabs/Touch"));
                touchPlay.transform.position = Camera.main.ScreenToWorldPoint(touch.position);
                break;
                case TouchPhase.Ended:
                Destroy(touchPlay);
                break;
            }
        }
    }
}
