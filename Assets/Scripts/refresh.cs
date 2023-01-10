using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refresh : MonoBehaviour
{   public GameObject checkpoint;
    public Vector3 stat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            gameObject.transform.position = new Vector3(checkpoint.transform.position.x, 10f,checkpoint.transform.position.z);
            this.transform.eulerAngles = stat;
        }
    }
}
