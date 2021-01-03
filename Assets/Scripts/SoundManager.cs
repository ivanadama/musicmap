using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public int maxSounds = 5;
    public float initialSoundDistance = 10f;
    public GameObject soundPrefab;

    Grid grid;
    Vector3 lastCell = Vector3.zero;
    List<GameObject> sounds;

    // Start is called before the first frame update
    void Start()
    {   
        sounds = new List<GameObject>();
        grid = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        // center the grid and init the sounds once the location is found
        if(transform.position == Vector3.zero){
            transform.position = Listener.getPosition();
            initSounds();
        }

        // check for cell changes
        Vector3 currentCell = grid.WorldToCell(Listener.getPosition());
        if(currentCell != lastCell) {
            // update sounds (also plays sounds)
            updateSounds(currentCell);
            // update cell
            lastCell = currentCell;

            print(lastCell);
        }
    }

    void initSounds(){
        
        for(int i = 0; i < maxSounds; i++) {

            // picking random points, later we can use specific GPS positions
            Vector3 rnd = Random.insideUnitSphere;
            Vector3 soundPos = new Vector3(rnd.x, 0, rnd.y) * initialSoundDistance + Listener.getPosition();

            GameObject sound = Instantiate(soundPrefab, soundPos, Quaternion.identity);

            // get cell position and calculate sound clip
            Vector3 startingCell = grid.WorldToCell(sound.transform.position);
            print(startingCell);

            // set the cell
            sound.GetComponent<SoundCell>().setStartingCell(startingCell);

            // add to list of sounds
            sounds.Add(sound);
        }
    }

    void updateSounds(Vector3 currentCell){
        // calculate change in cell
        var cellShift = lastCell - currentCell;

        foreach (var sound in sounds)
        {
            SoundCell sc = sound.GetComponent<SoundCell>();
            // shift the cell
            sc.shiftCell(cellShift);
            // play the updated sound
            sc.playSound(cellShift);
        }
    }
}
