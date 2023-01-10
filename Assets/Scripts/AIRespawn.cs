using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRespawn : MonoBehaviour
{   
    //Create new GameObject under the car
    //Put a box collider in front para maanalyzed kung bumangga or hindi
    //put this script on that new game Object with the Collider

    public GameObject enemy;

    //The countdown on how long it will wait to confirm na nabangga ngasiya before moving backward
    public float countDown;

    //Countdown how long it will move backward
    public float countDownPart2;

    //Controls how long it will wait before moving
    public bool countDownStart;
    
    //Controls how long it will move backward
    public bool countRestart;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("EnemyCar");
    }

    // Update is called once per frame
    void Update()
    {
        if(countDownStart){
            countDown += Time.deltaTime;
        }

        if(countDown >=1){
            //Teleport
            // enemy.transform.position = new Vector3(checkpoint.transform.position.x, 10f, checkpoint.transform.position.z);
            // this.transform.eulerAngles = stat;

            //Move BackWards
            gameObject.GetComponentInParent<CarAIEngine>().driveBackwards = true;
        }

        if(countRestart){
            countDownPart2 += Time.deltaTime;
        }

        if(countDownPart2 >=1.5){
            gameObject.GetComponentInParent<CarAIEngine>().driveBackwards = false;
            countRestart = false;
            countDownPart2 = 0;
        }

       
    }

    void OnTriggerEnter(Collider other){
        countDownStart = true;
    }

    void OnTriggerExit(Collider other){
        countDownStart = false;
        countDown = 0;
        countRestart = true;
    }

    // public void Teleport(){
    //         enemy.transform.position = new Vector3(checkpoint.transform.position.x, 10f, checkpoint.transform.position.z);
    //         this.transform.eulerAngles = stat;
    // }
}
