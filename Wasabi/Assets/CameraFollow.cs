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
    public Transform Target
    {
        private get { return target; }
        set { target = value;  }
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
