using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObserver_Light : MonoBehaviour {

    private Light _light;

    [SerializeField] private RotationController _rotationController;

    public float sizeA;
    public float sizeB;
    public float sizeC;

    public float intensityA;
    public float intensityB;
    public float intensityC;

    public Color colorA;
    public Color colorB;
    public Color colorC;

	// Use this for initialization
	void Start () {
        _light = GetComponent<Light>();

        Debug.Assert(_rotationController != null, "RotationController reference is missing");
	}
	
	// Update is called once per frame
	void Update () {
		if (_rotationController.rotAngle < 180)
        {
            _light.range = Mathf.Lerp(sizeA, sizeB, _rotationController.rotAngle / 180);
            _light.intensity = Mathf.Lerp(intensityA, intensityB, _rotationController.rotAngle / 180);
            _light.color = Color.Lerp(colorA, colorB, _rotationController.rotAngle / 180);
        }
        else
        {
            _light.range = Mathf.Lerp(sizeB, sizeC, (_rotationController.rotAngle - 180) / 180);
            _light.intensity = Mathf.Lerp(intensityC, intensityB, (_rotationController.rotAngle - 180) / 180);
            _light.color = Color.Lerp(colorB, colorC, (_rotationController.rotAngle - 180) / 180);
        }
	}
}
