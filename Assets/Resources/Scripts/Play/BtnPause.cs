using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPause : MonoBehaviour
{
    public bool isPause = false;
    Animator ani;
    private void Start() {
        ani = GetComponent<Animator>();
    }

    private void Update() {
        ani.SetBool("Pause", isPause);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        isPause = !isPause;
    }
}
