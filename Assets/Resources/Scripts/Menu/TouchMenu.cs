using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMenu : MonoBehaviour
{
    GameObject touchCheck;
    private void Start() {
        touchCheck = GameObject.FindGameObjectWithTag("TouchCheck");
    }

    private void Update() {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            touchCheck.transform.position = touchPos;
        }
    }
}
