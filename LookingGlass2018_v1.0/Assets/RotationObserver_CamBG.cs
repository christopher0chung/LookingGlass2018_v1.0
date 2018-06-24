using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObserver_CamBG : MonoBehaviour {

    public Color c1;
    public Color c2;

    public RotationController myRC;

    void Start()
    {
        Debug.Assert(myRC != null, "Missing Rotation Controller reference");
    }

	void Update () {
        HoloPlay.Capture.Instance.cam.backgroundColor = Color.Lerp(c1, c2, myRC.rotAngle / 360);
    }
}
