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
            // update sounds 

            // play chord

            // update cell
            print(lastCell - currentCell);
            lastCell = currentCell;
        }
    }

    void initSounds(){
        for(int i = 0; i < maxSounds; i++) {
            // pick random location around the listener, offset by sound distance
            Vector3 lPos = Listener.getPosition();
            Vector2 soundPos = Random.insideUnitCircle.normalized * initialSoundDistance + new Vector2(lPos.x, lPos.z);
            GameObject s1 = Instantiate(soundPrefab, Tools.vec2ToVec3(soundPos), Quaternion.identity);

            // for now change the pitch slightly
            s1.GetComponent<AudioSource>().pitch = Random.Range(0.85f,1.0f);

            // in the future each sound should have it's own little sound bank script
        }
    }
}
