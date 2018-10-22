using System;
using System.Collections;
//using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
//using USBInterface;

public class RotationController : MonoBehaviour {

    //#region Knob
    //private const ushort USB_VID = 0x077d;
    //private const ushort USB_PID = 0x0410;
    //private static volatile int angle_delta = 0;
    //public float angle_mult = 0.61290f;
    //public volatile bool button_state = false;
    //private static USBDevice dev;
    //private static DeviceScanner scanner;
    //public void handle(object s, USBInterface.ReportEventArgs a)
    //{
    //    // Debug.Log(string.Join(", ", a.Data));
    //    if (a.Data[0] == 0x1) button_state = true;
    //    else button_state = false;
    //    if (a.Data[1] == 0x1) angle_delta += 1;
    //    else if (a.Data[1] == 0xff) angle_delta -= 1;
    //}

    //public void enter(object s, EventArgs a)
    //{
    //    Debug.Log("device arrived");
    //    try
    //    {
    //        dev = new USBDevice(USB_VID, USB_PID, null, false, 6);
    //        dev.InputReportArrivedEvent += handle;
    //        // after adding the handle start reading
    //        dev.StartAsyncRead();
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError(e);
    //    }
    //}
    //public void exit(object s, EventArgs a)
    //{
    //    Debug.Log("device removed");
    //}


    //void OnApplicationQuit()
    //{
    //    if (scanner.isScanning)
    //    {
    //        scanner.StopAsyncScan();
    //    }
    //    if (dev.isOpen)
    //    {
    //        dev.StopAsyncRead();
    //        dev.Dispose();
    //    }
    //}

    //void Awake()
    //{
    //    scanner = new DeviceScanner(USB_VID, USB_PID);
    //    scanner.DeviceArrived += enter;
    //    scanner.DeviceRemoved += exit;
    //    scanner.StartAsyncScan();
    //}
    //#endregion

    //public HoloPlay.Capture myCap;

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

    public float newFOVA;
    public float newFOVB;
    public float newFOVC;

    public float newRelYPosA;
    public float newRelYPosB;
    public float newRelYPosC;

    public float newRelZPosA;
    public float newRelZPosB;
    public float newRelZPosC;

    public float xRotA;
    public float xRotB;
    public float xRotC;

    private Vector3 _rotToSet;
    private float _rotRateCalculated;

    public GameObject[] aObjs;
    public GameObject[] bObjs;

    public Camera cam;

    bool startMove;

	void Start () {
        _rotToSet = Vector3.zero;

        aObjs = GameObject.FindGameObjectsWithTag("A");
        bObjs = GameObject.FindGameObjectsWithTag("B");

	}

    void Update()
    {
        InputMethod();
        SetRotationAngle();
        AToBCamXfm();
        SwapObjs();
    }


    private void InputMethod ()
    {
        //if (Input.GetAxis("Mouse ScrollWheel") > 0)
        //{
        //    rotAngle += rotSpeedInput;
        //}
        //else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        //{
        //    rotAngle -= rotSpeedInput;
        //}

        //rotAngle += angle_delta * angle_mult;
        //angle_delta = 0;

        if (Input.GetKeyDown(KeyCode.S))
            startMove = true;

        if (startMove)
            rotAngle = Mathf.MoveTowards(rotAngle, 360, ((rotAngle * (360 - rotAngle) / 2000) + 20) * Time.deltaTime);

        rotAngle = Mathf.Clamp(rotAngle, 0, 360);
    }

    private void SetRotationAngle()
    {
        _rotToSet.y = rotAngle;

        transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.eulerAngles, _rotToSet, rotSpeedOutput * Mathf.Abs(transform.eulerAngles.y - _rotToSet.y)));
     
    }

    private void AToBCamXfm()
    {
        //if (rotAngle < 180)
        //{
        //    myCap.Size = Mathf.Lerp(sizeA, sizeB, rotAngle / 180);
        //    myCap.Fov = Mathf.Lerp(fovA, fovB, rotAngle / 180);
        //}
        //else
        //{
        //    myCap.Size = Mathf.Lerp(sizeB, sizeC, (rotAngle - 180) / 180);
        //    myCap.Fov = Mathf.Lerp(fovB, fovC, (rotAngle - 180) / 180);
        //}

        if (rotAngle < 180)
        {
            cam.fieldOfView = Mathf.Lerp(newFOVA, newFOVB, rotAngle / 180);

            Vector3 relPos = new Vector3(0, Mathf.Lerp(newRelYPosA, newRelYPosB, rotAngle / 180), Mathf.Lerp(newRelZPosA, newRelZPosB, rotAngle / 180));
            cam.transform.localPosition = relPos;

            Vector3 localRot = new Vector3(Mathf.Lerp(xRotA, xRotB, rotAngle / 180), 0, 0);
            cam.transform.localRotation = Quaternion.Euler(localRot);
        }
        else
        {
            //Debug.Log(rotAngle);
            cam.fieldOfView = Mathf.Lerp(newFOVB, newFOVC, (rotAngle - 180) / 180);

            Vector3 relPos = new Vector3(0, Mathf.Lerp(newRelYPosB, newRelYPosC, (rotAngle - 180) / 180), Mathf.Lerp(newRelZPosB, newRelZPosC, (rotAngle - 180) / 180));
            cam.transform.localPosition = relPos;

            Vector3 localRot = new Vector3(Mathf.Lerp(xRotB, xRotC, (rotAngle - 180) / 180), 0, 0);
            cam.transform.localRotation = Quaternion.Euler(localRot);
        }

    }

    private void SwapObjs()
    {
        ///Turning on and off A Objects
        if (rotAngle < 180)
        {
            foreach (GameObject a in aObjs)
            {
                a.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject a in aObjs)
            {
                a.SetActive(false);
            }
        }

        ///Turning on and off B Objects
        if (rotAngle < 180)
        {
            foreach (GameObject b in bObjs)
            {
                b.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject b in bObjs)
            {
                b.SetActive(true);
            }
        }
    }
}
