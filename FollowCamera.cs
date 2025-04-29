using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField, Tooltip("A value between 0 and 1 that defines the movement's speed")]
    private float ratio;
    /// <summary>A value between 0 and 1 that defines the movement's speed</summary>
    public float Ratio { get => ratio; set => ratio = Mathf.Clamp01(value); }
    public float minDistance;
    public Camera cam;
    public Camera Cam
    {
        get => cam;
        set
        {
            cam = value;
            camTrans = cam.transform;
        }
    }
    private Transform camTrans;

    [Header("Borders")]
    public bool bordersEnabled;
    [Tooltip("Creates an imaginary rectangle outside of which the camera can't go")]
    public Cardinals borders = new(-10, 6, 10, -6);

    void Start()
    {
        camTrans = cam.transform;
    }

    void FixedUpdate()
    {
        // Calculate the distance between the camera and the object
        Vector3 thisTrans = transform.position;
        thisTrans.z = camTrans.position.z;
        float distance = Vector3.Distance(thisTrans, camTrans.position);

        // Move the camera only if the distance exceeds the minimum distance
        if (distance > minDistance)
        {
            camTrans.position = Vector3.Slerp(camTrans.position, thisTrans, ratio);

            // Apply border constraints if enabled
            if (bordersEnabled)
            {
                Vector2 dimensions = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);

                // Clamp the camera's position within the defined borders
                float clampedX = Mathf.Clamp(camTrans.position.x, borders.left + dimensions.x, borders.right - dimensions.x);
                float clampedY = Mathf.Clamp(camTrans.position.y, borders.bottom + dimensions.y, borders.top - dimensions.y);
                camTrans.position = new Vector3(clampedX, clampedY, camTrans.position.z);
            }
        }
    }

    /// <summary>Creates an imaginary rectangle outside of which the camera can't go</summary>
    [System.Serializable]
    public struct Cardinals
    {
        public float left;
        public float top;
        public float right;
        public float bottom;

        public Cardinals(float west, float north, float east, float south)
        {
            left = west; top = north; right = east; bottom = south;
        }
    }

    private void OnValidate()
    {
        ratio = Mathf.Clamp01(ratio);
        if (bordersEnabled && cam != null)
        {
            float CamHeight = cam.orthographicSize * 2;
            borders.left = Mathf.Min(borders.left, borders.right - CamHeight * cam.aspect);
            borders.bottom = Mathf.Min(borders.bottom, borders.top - CamHeight);
        }
    }
}
