using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinishLine : MonoBehaviour
{
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Enemy"){
            count++;
            if(count == 1){
                other.GetComponent<timer>().record1();
            }else if(count == 2){
                other.GetComponent<timer>().record2();
            }else if(count == 3){
                other.GetComponent<timer>().record3();
                other.GetComponent<timer>().stopclocks();
                other.GetComponent<CarAIEngine>().isRacing = false;
                other.GetComponent<CarAIEngine>().maxMotorTorque = 0f;
            }
        }
    }
}
