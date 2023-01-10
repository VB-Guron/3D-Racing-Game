using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class session : MonoBehaviour
{
    public void CountDownToEndCredits(){
        
        StartCoroutine (startCounting());
    }

    private IEnumerator startCounting () {
            yield return new WaitForSeconds (3f);
    }
}
