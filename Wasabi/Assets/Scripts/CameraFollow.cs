using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public void Start()
    {
        transform.position = new Vector2(0, 0);
        Target = transform;
        Camera.main.orthographicSize = 70;
    }

    float minFov = 15f;
    float maxFov = 70f;
    float sensitivity = -10f;
    // Update is called once per frame
    void Update()
    {
        float fov = Camera.main.orthographicSize;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.orthographicSize = fov;
    }

    public Transform Target
    {
        private get { return target; }
        set { target = value;  }
    }
    void LateUpdate()   
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition = new Vector3(smoothedPosition.x, smoothedPosition.y, offset.z);
        transform.position = smoothedPosition;
    }
}
