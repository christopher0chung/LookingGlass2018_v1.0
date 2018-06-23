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

	void Start () {
        _rotToSet = Vector3.zero;
	}
	
	void Update () {
        InputMethod();
        SetRotationAngle();
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
