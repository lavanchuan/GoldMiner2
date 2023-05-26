using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeTNT : MonoBehaviour
{
    private void Awake() {
        StartCoroutine(AniDestroy());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag != "Limit" && other.collider.tag != "Hook"){
            Destroy(other.gameObject);
        }
    }

    IEnumerator AniDestroy(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
