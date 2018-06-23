using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    public HoloPlay.Capture myCap;

    public float rotAngle { get; private set; }
    public float rotSpeedInput;
    public float rotSpeedOutput;

    private Vector3 _rotToSet;
    private float _rotRateCalculated;

    public GameObject[] aObjs;
    public GameObject[] bObjs;

	void Start () {
        _rotToSet = Vector3.zero;

        aObjs = GameObject.FindGameObjectsWithTag("A");
        bObjs = GameObject.FindGameObjectsWithTag("B");

	}

    void Update()
    {
        InputMethod();
        SetRotationAngle();
        print(transform.rotation);


        ///Turning on and off A Objects
        if (transform.rotation.y <= 0.7f) { 
            foreach (GameObject a in aObjs)
            {
                a.SetActive(true);
            }
        } else {
            foreach (GameObject a in aObjs)
            {
                a.SetActive(false);
            }
        }

        ///Turning on and off B Objects
        if (transform.rotation.y >= 0.7f)
        {
            foreach (GameObject b in bObjs)
            {
                b.SetActive(true);
            }
        } else {
            foreach (GameObject b in bObjs)
            {
                b.SetActive(false);
            }
        }

    }


    private void InputMethod ()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            rotAngle += rotSpeedInput;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            rotAngle -= rotSpeedInput;
        }

        rotAngle = Mathf.Clamp(rotAngle, 0, 360);
    }

    private void SetRotationAngle()
    {
        _rotToSet.y = rotAngle;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_rotToSet), rotSpeedOutput);
     
    }
}
