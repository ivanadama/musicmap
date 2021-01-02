using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{
    private static SoundBank _instance;
    public static SoundBank Instance { get { return _instance;} }

    private void Awake(){
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // using two banks to represent lat and lng movement
    AudioClip[] bankA;
    AudioClip[] bankB;
    void Start()
    {
        // load clips (maybe better to load references to the clips?)
        var clips = Resources.LoadAll("Sounds", typeof(AudioClip));

        // allocate to the banks somehow
        foreach (var c in clips){print(c.name);}
    }

    public void updateAudioSource(){

    }
}
