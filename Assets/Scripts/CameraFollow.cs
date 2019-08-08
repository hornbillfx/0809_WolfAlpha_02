using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void FixedUpdate()
    {

        Vector3 desiredPos = target.position + offset;
        Vector3 smooothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smooothedPos;

        //transform.LookAt(target);
    }


}
