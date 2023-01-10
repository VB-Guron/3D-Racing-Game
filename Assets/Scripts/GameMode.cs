using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour

{   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadLevel1(){
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2(){
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3(){
        SceneManager.LoadScene("Level3");
    }

    public void TryAgain(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");

    }

    public void DeleteSavedRecord(){
        PlayerPrefs.DeleteAll();
    }

    public void Quit(){
        Application.Quit();
    }
}
