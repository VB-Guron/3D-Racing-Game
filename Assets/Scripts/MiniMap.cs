using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer line;
    private GameObject TrackPath;

    public GameObject LocalPlayer;
    public GameObject LocalEnemy;
    public GameObject MiniMapCam;
    public GameObject Player;
    public GameObject Enemy;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        TrackPath = this.gameObject;

        int num_of_path = TrackPath.transform.childCount;
        line.positionCount = num_of_path+1;

        for(int x = 0; x < num_of_path; x++){
            line.SetPosition(x, new Vector3(TrackPath.transform.GetChild(x).transform.position.x,20,TrackPath.transform.GetChild(x).transform.position.z));
        }

        line.SetPosition(num_of_path, line.GetPosition(1));

        line.startWidth = 25f;
        line.endWidth = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        MiniMapCam.transform.position = (new Vector3(LocalPlayer.transform.position.x,MiniMapCam.transform.position.y, LocalPlayer.transform.position.z));

        Player.transform.position = (new Vector3(LocalPlayer.transform.position.x,Player.transform.position.y, LocalPlayer.transform.position.z));
        
        Enemy.transform.position = (new Vector3(LocalEnemy.transform.position.x,Player.transform.position.y, LocalEnemy.transform.position.z));

        MiniMapCam.transform.rotation = Quaternion.Euler(90f, LocalPlayer.transform.eulerAngles.y, 0f);
        Player.transform.rotation = Quaternion.Euler(90f, LocalPlayer.transform.eulerAngles.y, 0f);
        Enemy.transform.rotation = Quaternion.Euler(90f, LocalEnemy.transform.eulerAngles.y, 0f);
    }
}
