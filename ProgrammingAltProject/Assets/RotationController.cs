using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using USBInterface;

public class RotationController : MonoBehaviour
{

    #region Knob
    private const ushort USB_VID = 0x077d;
    private const ushort USB_PID = 0x0410;
    private static volatile int angle_delta = 0;
    public float angle_mult = 11.61290f;
    public volatile bool button_state = false;
    private static USBDevice dev;
    private static DeviceScanner scanner;
    public void handle(object s, USBInterface.ReportEventArgs a)
    {
        // Debug.Log(string.Join(", ", a.Data));
        if (a.Data[0] == 0x1) button_state = true;
        else button_state = false;
        if (a.Data[1] == 0x1) angle_delta += 1;
        else if (a.Data[1] == 0xff) angle_delta -= 1;
    }

    public void enter(object s, EventArgs a)
    {
        Debug.Log("device arrived");
        try
        {
            dev = new USBDevice(USB_VID, USB_PID, null, false, 6);
            dev.InputReportArrivedEvent += handle;
            // after adding the handle start reading
            dev.StartAsyncRead();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    public void exit(object s, EventArgs a)
    {
        Debug.Log("device removed");
    }


    void OnApplicationQuit()
    {
        if (scanner.isScanning)
        {
            scanner.StopAsyncScan();
        }
        if (dev.isOpen)
        {
            dev.StopAsyncRead();
            dev.Dispose();
        }
    }

    void Awake()
    {
        scanner = new DeviceScanner(USB_VID, USB_PID);
        scanner.DeviceArrived += enter;
        scanner.DeviceRemoved += exit;
        scanner.StartAsyncScan();
    }
    #endregion

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

    public GameObject[] aObjs;
    public GameObject[] bObjs;

    void Start()
    {
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


    private void InputMethod()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            rotAngle += rotSpeedInput;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            rotAngle -= rotSpeedInput;
        }

        rotAngle += angle_delta * angle_mult;
        angle_delta = 0;

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
