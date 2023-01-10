using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class endGameController : MonoBehaviour
{


    public UnityEvent EndCreditsGeneral;
    public UnityEvent EndCreditsWin;
    public UnityEvent EndCreditsLose;
    public int scorePlayer;
    public int scoreEnemy;

    public GameObject winBanner;
    public GameObject loseBanner;

    public GameObject PlayerCar;
    public GameObject EnemyCar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }


    public void playEndCredits(){
        EndCreditsGeneral.Invoke();
        if(scorePlayer < scoreEnemy){
            EndCreditsLose.Invoke();
        }else if(scoreEnemy < scorePlayer){
            EndCreditsWin.Invoke();
        }
    }
}
