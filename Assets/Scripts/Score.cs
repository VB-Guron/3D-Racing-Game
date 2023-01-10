using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{   
    public int Checkpoints;
    public int Laps;
    public Animator anim;
    public GameObject NumberOfCheckpoint;
    public GameObject FinishLine;
    public GameObject clock;

    // Start is called before the first frame update
    void Start()
    {
        Checkpoints = 0;
        Laps = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        if(Checkpoints == NumberOfCheckpoint.GetComponent<FinishLine>().checkpoints.Length){
            FinishLine.SetActive(true);
        }
    }

    public void increaseLaps(){
        anim.SetTrigger("showIt");
        Laps++;
        if(Laps == 1){
            clock.GetComponent<timer>().record1();
        }
        if(Laps == 2){
                        clock.GetComponent<timer>().record2();

        }
        if(Laps == 3){
                        clock.GetComponent<timer>().record3();

        }
    }

    public void reachedACheckpoint(){
        Checkpoints++;
    }

    public void resetCheckpointConter(){
        Checkpoints = 0;
    }
}
