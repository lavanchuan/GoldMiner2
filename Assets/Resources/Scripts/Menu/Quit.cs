using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Quiting");
        // System.Diagnostics.Process.GetCurrentProcess().Kill();
        Application.Quit(1);
    }
}
