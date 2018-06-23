using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    public HoloPlay.Capture myCap;

    public float rotAngle { get; private set; }
    public float rotSpeedInput;
    public float rotSpeedOutput;

    [Header("Tuning Variables")]
    public float sizeA;
    public float sizeB;
    public float sizeC;
    public float fovA;
    public float fovB;
    public float fovC;

    private Vector3 _rotToSet;
    private float _rotRateCalculated;

	void Start () {
        _rotToSet = Vector3.zero;
	}
	
	void Update () {
        InputMethod();
        SetRotationAngle();
        AToBCamXfm();
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

    private void AToBCamXfm()
    {
        if (rotAngle < 180)
        {
            myCap.Size = Mathf.Lerp(sizeA, sizeB, rotAngle / 180);
            myCap.Fov = Mathf.Lerp(fovA, fovB, rotAngle / 180);
        }
        else
        {
            myCap.Size = Mathf.Lerp(sizeB, sizeC, (rotAngle - 180) / 180);
            myCap.Fov = Mathf.Lerp(fovB, fovC, (rotAngle - 180) / 180);
        }
    }
}
