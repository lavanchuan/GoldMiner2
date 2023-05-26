using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMute : MonoBehaviour
{
    Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        ani.SetBool("IsMute", GameObject.FindGameObjectWithTag("MainCamera")
        .GetComponent<AudioMute>().isMuteMusic);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Touch")
        {

            GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<AudioMute>().isMuteMusic = !GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<AudioMute>().isMuteMusic;

            // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute
            // = !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute;

            // GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().mute
            // = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute;

            // other.transform.position = new Vector3(transform.position.x + 100,
            // transform.position.y + 100, 0);
        }
    }
}
