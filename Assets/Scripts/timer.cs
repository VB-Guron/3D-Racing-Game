using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public string levelNumber;
    public string textVer;
    public string valueVer;


    public string CarTime;
    public Stopwatch clock;
    public Stopwatch clock2;
    public TimeSpan tspan;
    public TimeSpan compare1;
    public TimeSpan compare2;

    public string lap1;
    public string lap2;
    public string lap3;
    public string total;

    public Text Lap1;
    public Text Lap2;
    public Text Lap3;
    public Text OnScreenMoving;
    public Text Total;

    public Text FastestPlayer;
    public Text FastestEnemy;

    public Text Updates;

    public Animator anim;

    public void Start(){
        
        clock = new Stopwatch();
        clock2 = new Stopwatch();
        tspan = new TimeSpan();

        if(gameObject.tag == "Player"){
            Updates = GameObject.Find("PlayerUpdate").GetComponent<Text>();
            anim = GameObject.Find("PlayerUpdates").GetComponent<Animator>();
            if(!PlayerPrefs.HasKey(levelNumber + gameObject.tag + "text")){
                PlayerPrefs.SetString(levelNumber + gameObject.tag + "text", "0");
                PlayerPrefs.SetFloat(levelNumber + gameObject.tag + "value", 0);
                UnityEngine.Debug.Log("Value Set");
                UnityEngine.Debug.Log( gameObject.tag + " || " + PlayerPrefs.GetFloat(levelNumber+gameObject.tag + "value"));
                UnityEngine.Debug.Log(PlayerPrefs.GetFloat(levelNumber+gameObject.tag + "text"));

            }
        }else if(gameObject.tag == "Enemy"){
            Updates = GameObject.Find("EnemyUpdate").GetComponent<Text>();
            anim = GameObject.Find("EnemyUpdates").GetComponent<Animator>();
            if(!PlayerPrefs.HasKey(levelNumber + gameObject.tag + "text")){
                PlayerPrefs.SetString(levelNumber + gameObject.tag + "text", "0");
                PlayerPrefs.SetFloat(levelNumber + gameObject.tag + "value", 0);
                UnityEngine.Debug.Log("Value Set");
            }

            UnityEngine.Debug.Log( gameObject.tag + " || " + PlayerPrefs.GetFloat(levelNumber+gameObject.tag + "value"));

        }

    }

    public void startTimer(){
        clock.Start();
        clock2.Start();
    }

    public void Update(){
        tspan = clock2.Elapsed;
        if(gameObject.tag == "Player"){
            OnScreenMoving.text = String.Format ( " {0:00}:{1:00}:{2:00}.{3:00} " ,
        tspan.Hours , tspan.Minutes , tspan.Seconds ,  tspan.Milliseconds / 10 ) ;
        }

    }

    public void stopTimer(){
        clock.Stop();
    }

    public void record1(){
        tspan = clock.Elapsed;
        lap1 = String.Format ( " {0:00}:{1:00}:{2:00}.{3:00} " ,
tspan.Hours , tspan.Minutes , tspan.Seconds ,  tspan.Milliseconds / 10 ) ;

        Lap1.text = lap1;
        
        Updates.text = gameObject.tag + " Lap 1: " + lap1;

        if(getFastTime(levelNumber+gameObject.tag+"value") == 0 || getFastTime(levelNumber+gameObject.tag+"value") > (float)tspan.TotalSeconds){
            saveTime(lap1, (float)tspan.TotalSeconds);
            Updates.text = gameObject.tag + " Lap 1: " + lap1 + " New Record!";
        }

      



        anim.SetTrigger("showIt");

        clock = Stopwatch.StartNew();

    }

    public void record2(){
        tspan = clock.Elapsed;
        compare2 = clock.Elapsed;
        
        clock = Stopwatch.StartNew();
        lap2 = String.Format ( " {0:00}:{1:00}:{2:00}.{3:00} " ,
tspan.Hours , tspan.Minutes , tspan.Seconds ,  tspan.Milliseconds / 10 ) ;

        Updates.text = gameObject.tag + " Lap 2: " + lap2;
        Lap2.text = lap2;

        if(getFastTime(levelNumber+gameObject.tag+"value") == 0 || getFastTime(levelNumber+gameObject.tag+"value") > (float)tspan.TotalSeconds){
            saveTime(lap2, (float)tspan.TotalSeconds);
            Updates.text = gameObject.tag + " Lap 2: " + lap2 + " New Record!";
        }
        anim.SetTrigger("showIt");
        

        

        
        

    }

    public void record3(){
        tspan = clock.Elapsed;
        
        clock = Stopwatch.StartNew();
        lap3 = String.Format ( " {0:00}:{1:00}:{2:00}.{3:00} " ,
tspan.Hours , tspan.Minutes , tspan.Seconds ,  tspan.Milliseconds / 10 ) ;

        Updates.text = gameObject.tag + " Lap 3: " +  lap3;
        Lap3.text = lap3;

        if(getFastTime(levelNumber+gameObject.tag+"value") == 0 || getFastTime(levelNumber+gameObject.tag+"value") > (float)tspan.TotalSeconds){
            saveTime(lap3, (float)tspan.TotalSeconds);
            Updates.text = gameObject.tag + " Lap 3: " + lap3 + " New Record!";
        }
        anim.SetTrigger("showIt");



    }

    public void stopclocks(){
        clock.Stop();
        clock2.Stop();
        tspan = clock2.Elapsed;
        total = String.Format ( " {0:00}:{1:00}:{2:00}.{3:00} " ,
        tspan.Hours , tspan.Minutes , tspan.Seconds ,  tspan.Milliseconds / 10 ) ;

        Total.text = total;
        
        updateFastedRecord();
    }

    public void saveTime(string textVer, float time){
        PlayerPrefs.SetString(levelNumber + gameObject.tag + "text", textVer);
        PlayerPrefs.SetFloat(levelNumber + gameObject.tag + "value", time);
    }

    public string showTime(string name){
        return PlayerPrefs.GetString(name);
    }

    public float getFastTime(string name){
        return PlayerPrefs.GetFloat(name);
    }

    public void updateFastedRecord(){
        if(gameObject.tag == "Player"){
            FastestPlayer.text = showTime(levelNumber+gameObject.tag+"text");
        }
        if(gameObject.tag == "Enemy"){
            FastestEnemy.text = showTime(levelNumber+gameObject.tag+"text");
        }
    }


        public void resetScore(){
        if(gameObject.tag == "Player"){
            FastestPlayer.text = "00:00:00.00";
        }
        if(gameObject.tag == "Enemy"){
            FastestEnemy.text = "00:00:00.00";
        }
    }
    


}
