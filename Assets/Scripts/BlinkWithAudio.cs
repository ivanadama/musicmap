using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkWithAudio : MonoBehaviour
{
    AudioSource aud;
    private bool blinking = false;
    private Material mat;

    void Start(){
        aud = GetComponent<AudioSource>();
        mat = GetComponent<Renderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aud.time <= 0.1f && !blinking) {
            StartCoroutine(Blink());
            blinking = true;
        }
    }

    IEnumerator Blink(){
        float t = 0f;
        while (t < 1f){
            Color col = Color.Lerp(Color.white, Color.black, t);
            mat.SetColor("_Color", col);
            t += Time.deltaTime * 2f;
            yield return new WaitForEndOfFrame();
        }

        blinking = false;
        
    }
}
