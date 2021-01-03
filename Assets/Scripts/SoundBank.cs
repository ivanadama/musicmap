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
    List<AudioClip> bankA;
    List<AudioClip> bankB;
    void Start()
    {
        // load clips (maybe better to load references to the clips?)
        var clips =  Resources.LoadAll("Sounds", typeof(AudioClip));

        bankA = new List<AudioClip>();
        bankB = new List<AudioClip>();

        // allocate to the banks (odds n evens)
        int i = 0;
        foreach (AudioClip c in clips){
            if(i%2 == 0){
                bankA.Add(c);
            } else {
                bankB.Add(c);
            }
            i++;
        }
    }

    public AudioClip[] getClips(int clipA, int clipB){
        // keep clips within bank bounds
        clipA = clipA % bankA.Count;
        clipB = clipB % bankB.Count;
        // subtract negatives from the top
        if(clipA < 0) clipA = bankA.Count + clipA;
        if(clipB < 0) clipB = bankB.Count + clipB;

        AudioClip[] clips = { bankA[clipA], bankB[clipB] };
        return clips;
    }
}
