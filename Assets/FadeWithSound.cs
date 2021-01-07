using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWithSound : MonoBehaviour
{
    [SerializeField] float fadeTime = 1f;
    SpriteRenderer sr;
    Color fadeColor;
    float fadeAmount = 0f;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        fadeColor = Color.white;
        fadeColor.a = fadeAmount; 
    }

    // Update is called once per frame
    void Update()
    {
        fadeColor.a = fadeAmount;

        sr.color = fadeColor;

        // update fade
        fadeAmount = Mathf.Clamp(fadeAmount - (Time.deltaTime * fadeTime), 0f, 1f);
    }

    public void Blink(){
        fadeAmount = 1f;
    }
}
