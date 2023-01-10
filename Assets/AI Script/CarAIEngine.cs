using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 45f;
    public float turnSpeed = 5f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public float maxMotorTorque = 80;
    public float maxBreakTorque = 150;
    public float currentSpeed;
    public float maximumSpeed = 120f;
    public Vector3 centerOfMass;
    public bool isBreaking = false;
    public Rigidbody rigidBody;

    [Header("Sensors")]
    public float sensorLength = 3f;
    public Vector3 frontSensorPos = new Vector3 (0,0,0);
    public float frontSideSensorPos = 0.2f;
    public float frontSensorAngle = 30f;
    public bool driveBackwards;


    List<Transform> nodes;
    public int currentNode = 0;
    public bool avoiding = false;
    public float targetSteerAngle = 0;
    public bool isRacing = false;

    public bool gameStart = false;

    void Start()
    {
        driveBackwards = false;
        rigidBody.centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
        StartCoroutine(timerStart());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(gameStart){
            if (isRacing)
            {
                Sensors();
                ApplySteer();
                Drive();
                CheckWaypointDistance();
                Braking();
                LerpToSteerAngle();
            }
            else
            {
                isBreaking = true;
                Braking();
                Drive();
            }
        }
    }


    IEnumerator timerStart(){
        yield return new WaitForSeconds(3);
        gameStart=true;
    }

    void Sensors()
	{
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position + frontSensorPos;
        sensorStartPos += transform.forward * frontSensorPos.z;
        sensorStartPos += transform.up * frontSensorPos.y;
        float avoidMultiplier = 0;
        avoiding = false;

        //front right sensor
        sensorStartPos += transform.right * frontSideSensorPos;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 1f;
            }
        }
        


        //front right angle sensor
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }


        //front left sensor
        sensorStartPos -= transform.right * frontSideSensorPos * 2;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 1f;
            }
        }
        

        //front left angle sensor
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }

        if(avoidMultiplier == 0)
		{
            //front center sensor
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Player"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoiding = true;
                    if(hit.normal.x < 0)
					{
                        avoidMultiplier = -1;
					}
					else
					{
                        avoidMultiplier = 1;
					}
                }
            }
        }

        if (avoiding)
		{
            targetSteerAngle = maxSteerAngle * avoidMultiplier;

        }
        
    }

    void ApplySteer()
	{
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        targetSteerAngle = newSteer;
	}

    void Drive()
	{
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if(currentSpeed < maximumSpeed && !isBreaking)
		{
            if(!driveBackwards){
                wheelFL.motorTorque = maxMotorTorque;
                wheelFR.motorTorque = maxMotorTorque;
            }else{
                Backwards();
            }
        }
		else
		{
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }

        
	}

    void CheckWaypointDistance()
	{
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 10f)
		{
            if(currentNode  == nodes.Count - 1)
			{
                currentNode = 0;
			}
			else
			{
                currentNode++;
			}
		}
	}

    void Braking()
	{
		//Put the glow effects using unity particles later on
		if (isBreaking)
		{
            wheelRL.brakeTorque = maxBreakTorque;
            wheelRR.brakeTorque = maxBreakTorque;
		}
		else
		{
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
	}

    void LerpToSteerAngle()
	{
        wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        wheelFR.steerAngle = Mathf.Lerp(wheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }

    void Backwards(){
        wheelFL.motorTorque = -maxMotorTorque;
        wheelFR.motorTorque = -maxMotorTorque;
        targetSteerAngle = -targetSteerAngle;
    }
}
