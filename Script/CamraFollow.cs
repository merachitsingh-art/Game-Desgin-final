using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // 
    public float smoothSpeed = 5f;    // 
    public Vector3 offset = new Vector3(0f, 0f, -10f); // 

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate where the camera wants to go
            Vector3 desiredPosition = target.position + offset;
            
            // move from current position to desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            
            transform.position = smoothedPosition;
        }
    }
}
