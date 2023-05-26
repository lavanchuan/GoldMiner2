using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    Animator ani;
    private void Start() {
        ani = GetComponent<Animator>();
    }

private void Update() {
    ani.SetBool("IsMute", GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute);
}

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Touch")
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute
            = !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute;
        }
    }
}
