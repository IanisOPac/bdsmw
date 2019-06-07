using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWheel : MonoBehaviour {

    float minFov = 15f;
    float maxFov = 70f;
    float sensitivity = -10f;
	// Update is called once per frame
	void Update () {
        float fov = Camera.main.orthographicSize;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.orthographicSize = fov;
	}
}
