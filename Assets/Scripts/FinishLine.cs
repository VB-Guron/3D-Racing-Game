using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour
{   
    public GameObject[] checkpoints;
    public GameObject endBanner;
    public GameObject clock;
    public GameObject EnemyStatus;
    public GameObject Win;
    public GameObject Lose;
    public GameObject curtain;
    public GameObject EndGame;
    

    public UnityEvent TriggerEnding;


    // Update is called once per frame
   void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Player"){
            other.GetComponent<Score>().increaseLaps();
            if(other.GetComponent<Score>().Laps < 3){
                other.GetComponent<Score>().resetCheckpointConter();
                for(int i = 0; i < checkpoints.Length; i++){
                    checkpoints[i].SetActive(true);
                }
            }else if(other.GetComponent<Score>().Laps >= 3){
                //EndGame
                
                clock.GetComponent<timer>().stopclocks();
                //check enemy
                if(EnemyStatus.GetComponent<EnemyFinishLine>().count < 3){
                    Win.SetActive(true);
                }else{
                    Lose.SetActive(true);
                }

                endBanner.SetActive(true);
                if(SceneManager.GetActiveScene().name == "Level3"){
                    TriggerEnding.Invoke();
                    StartCoroutine (startCounting());
                }
            }
        }
        
	}

    private IEnumerator startCounting () {
            yield return new WaitForSeconds (3f);
            curtain.GetComponent<Animator>().SetTrigger("rollCredits");
            yield return new WaitForSeconds (1.5f);
            EndGame.GetComponent<endGameController>().playEndCredits();
            curtain.GetComponent<Animator>().SetTrigger("ready");


    }
}
