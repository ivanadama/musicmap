using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCell : MonoBehaviour
{
    Vector3 cell = Vector3.zero;
    AudioSource audioSource;
    SoundBank bank;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        bank = GameObject.FindGameObjectWithTag("SoundBank").GetComponent<SoundBank>();
    } 

    public void setStartingCell(Vector3 _cell){
        cell = _cell;
    }

    public Vector3 getCell(){
        return cell;
    }

    public void shiftCell(Vector3 shift){
        cell = cell + shift;
    }

    public void playSound(Vector3 soundMask){
        // get clips
        var a = (int)cell.x;
        var b = (int)cell.z;

        var clips = bank.getClips(a, b);

        if(soundMask.x != 0)
            audioSource.PlayOneShot(clips[0]);
        if(soundMask.z != 0)
            audioSource.PlayOneShot(clips[1]);
        
    }
}
