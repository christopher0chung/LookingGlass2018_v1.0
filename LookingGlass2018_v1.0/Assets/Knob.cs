using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using USBInterface;

public class Knob : MonoBehaviour
{
    private const ushort USB_VID = 0x077d;
    private const ushort USB_PID = 0x0410;
    private static volatile int angle_delta = 0;
    public float angle_mult = 11.61290f;
    public float angle = 0;
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

    void Update()
    {
        angle += angle_delta * angle_mult;
        angle_delta = 0;
        gameObject.transform.localRotation = Quaternion.Euler((button_state ? 180 : 0), angle, 0);
    }
}
