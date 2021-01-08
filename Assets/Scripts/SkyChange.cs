using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyChange : MonoBehaviour
{
    Material sky;
    public Material dayMat, nightMat;

    public float transitionTime = 5f;
    public string dayStart = "06:00";
    public string nightStart = "17:35";

    TimeSpan dayTime, nightTime, now;

    DateTime currentTime;
    string state = "day";

    Color[] dayColors = new Color[3];
    Color[] nightColors = new Color[3];
    
    void Start()
    {
        sky = RenderSettings.skybox;

        // grab colors from individual materials
        initSkyColors();

        // set initial sky color
        now = DateTime.Now.TimeOfDay;
        if (now >= dayTime && now < nightTime){
            setSkyColor(dayColors);
            state = "day";
        } else {
            setSkyColor(nightColors);
            state = "night";
        }  

        // state = "day";      
    }

    // Update is called once per frame
    void Update()
    {
        // debug test
        if(Input.GetKeyUp(KeyCode.Z)) {
            StartCoroutine( ChangeColor(dayColors, nightColors, 5f));
        }

        if(Input.GetKeyUp(KeyCode.X)) {
            StartCoroutine( ChangeColor(nightColors, dayColors, 5f));
        }

        now = DateTime.Now.TimeOfDay;
        if ((now >= dayTime && now < nightTime) && state == "night"){
            // night to day
            StartCoroutine( ChangeColor(nightColors, dayColors, transitionTime));
            state = "day";
        } else if((now < dayTime || now >= nightTime) && state == "day") {
            // day to night
            StartCoroutine( ChangeColor(dayColors, nightColors, transitionTime));
            state = "night";
        } 
    }

    IEnumerator ChangeColor(Color[] from, Color[] to, float lerpTime){
        float timeElapsed = 0;
        Color[] newColor = new Color[3];

        while ( timeElapsed < lerpTime ){
            // calc new color
            for(int i = 0; i < 3; i++){
                newColor[i] = Color.Lerp(from[i], to[i], timeElapsed/lerpTime);
            }
            // set sky
            setSkyColor(newColor);

            // update time
            timeElapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        setSkyColor(to);
    }

    void initSkyColors(){

        dayColors[0] = dayMat.GetColor("_TopColor");
        dayColors[1] = dayMat.GetColor("_MiddleColor");
        dayColors[2] = dayMat.GetColor("_BottomColor");

        nightColors[0] = nightMat.GetColor("_TopColor");
        nightColors[1] = nightMat.GetColor("_MiddleColor");
        nightColors[2] = nightMat.GetColor("_BottomColor");
    }

    void setSkyColor(Color[] skyColor){
        sky.SetColor("_TopColor",skyColor[0]);
        sky.SetColor("_MiddleColor",skyColor[1]);
        sky.SetColor("_BottomColor",skyColor[2]);
    }
}
